
#include <SPI.h>
boolean debug = false; // for verbose output
int CS_ARM = 4;
int CS_RA = 3;
int CS_AUT = 5;
int CS = 3; 
int MAX_TRIES = 10;
int MEASUREMENT_COUNT = 1;
uint16_t ABSposition_last = 0;
uint8_t temp[2];

float deg = 0.00;

void setup()
{
  // Configure outputs
  pinMode(CS_ARM,OUTPUT);
  pinMode(CS_AUT,OUTPUT);
  pinMode(CS_RA,OUTPUT);
   
  // Disable all 3 while we configure the SPI bus 
  digitalWrite(CS_ARM,HIGH);
  digitalWrite(CS_AUT,HIGH);
  digitalWrite(CS_RA,HIGH);
  
  // Configure SPI
  SPI.begin();
  SPI.setBitOrder(MSBFIRST);
  SPI.setDataMode(SPI_MODE0);
  SPI.setClockDivider(SPI_CLOCK_DIV32);
  Serial.begin(115200);
  Serial.flush();
  delay(2000);
  SPI.end();
  if (debug) {
    Serial.println("started");
  }
}

void loop()
{ 
  byte command = 0;
  int stat = 0;
  double start_time;
  
  if (Serial.available()) {
    start_time = micros();
     command = Serial.read();      
     if (debug) {
       Serial.print("Got command");
       Serial.println(command,HEX); 
     }
   }   
   if (command == 0x31) { // ASCII '1'
     CS = CS_ARM;  
   } else if (command == 0x32) { //ASCII '2'
     CS = CS_RA;
   } else if (command == 0x33) { //ASCII '3'
     CS = CS_AUT;
   } else if (command == 0x34) { //ASCII '4'
     stat = getStatus();
     
     // Send response back to master
     Serial.write(command);
     Serial.write(","); 
     Serial.write(stat); 
     Serial.write("\n"); 

     CS = -1;
   } else if (command == 0x35) { // Set Encoder 1 (ARM) Zero ASCII '5'
     uint8_t r = setZero(CS_ARM);  
     // Replies with "command, 0x80" if success 
     Serial.write(command);
     Serial.write(",");
     Serial.print(r,HEX);
     Serial.write("\n");
     CS = -1;
   } else if (command == 0x36) { // Set Encoder 2 (RA) Zero  ASCII '6'
     uint8_t r = setZero(CS_RA);     
     // Replies with "command, 0x80" if success 
     Serial.write(command);
     Serial.write(",");
     Serial.print(r,HEX);
     Serial.write("\n");
     CS = -1;
   } else if (command == 0x37) { // Set Encoder 3 (AUT) Zero ASCII '7'
     uint8_t r = setZero(CS_AUT); 
     // Replies with "command, 0x80" if success 
     Serial.write(command);
     Serial.write(",");
     Serial.print(r,HEX);
     Serial.write("\n"); 
     CS = -1;
   } else {
   //  Serial.println(command);
     CS = -1;  
   }
   
   if (CS != -1) {
     float pos_avg = 0;
     int pos = 0;
     int fail_count = 0;
     for (int i=0; i<MEASUREMENT_COUNT && fail_count<MAX_TRIES; i++) { // get 10 measurements, or 10 fails then stop
        pos = getPosition();
        if (pos == -1) { // encoder error, try again
          fail_count++; 
          i--;
        } else {
          pos_avg += pos;          
        }
        if (debug) Serial.println(pos,DEC); 
     }
     if (fail_count == MAX_TRIES) {
       pos_avg = -1;
     } else {
       pos_avg /= (float)MEASUREMENT_COUNT;
     }
     if (debug) {
       Serial.print("Average: " );
       Serial.println(pos_avg,DEC); 
     }
     
     // Send response back to master    
     Serial.write(command);
     Serial.write(",");
     Serial.print((int)pos_avg,DEC);
     Serial.write("\n");
     
     // Send time delay back 
     /*
     double stop_time = micros() - start_time;
     Serial.print("T,");
     Serial.println(stop_time,DEC); 
     */
   }
 
}

int getPosition() { // gets position of currently assigned Encoder, stored 2 bytes
   uint8_t recieved = 0xA5;
   int ABSposition = -1;

   SPI.begin();    //start transmition
   //digitalWrite(CS,LOW);
   
   SPI_T(0x10);   //issue read command
   recieved = SPI_T(0x00);    //issue NOP to check if encoder is ready to send
   
   if (debug) {
     Serial.print("received byte ");
     Serial.println(recieved);  
   }
   
   int fail_count = 0;
   while (recieved != 0x10 && fail_count<MAX_TRIES)    //loop while encoder is not ready to send
   {
     recieved = SPI_T(0x00);    //cleck again if encoder is still working 
     if (debug) {
       Serial.print("received byte ");
       Serial.println(recieved,HEX);  
     }
     fail_count++;
     delay(2);    //wait a bit
   }

   temp[0] = SPI_T(0x00);    //Recieve MSB
   temp[1] = SPI_T(0x00);    // recieve LSB   
   if (debug) {
       Serial.print("msb ");
       Serial.println(temp[0],HEX);  
       Serial.print("lsb ");
       Serial.println(temp[1],HEX);  
   }
   
   
//   digitalWrite(CS,HIGH);  
   SPI.end();    //end transmition 
   
   temp[0] &=~ 0xF0;    //mask out the first 4 bits
   ABSposition = temp[0] << 8;    //shift MSB to correct ABSposition in ABSposition message
   ABSposition += temp[1];    // add LSB to ABSposition message to complete message
   
   // If result is incorrect, or we didn't get a response from the encoder, send back -1
   if (ABSposition > 4096 || ABSposition < 0 || fail_count == MAX_TRIES) {
     ABSposition = -1;  
   }
   return ABSposition;
}



// TODO: check all 3 connections, give status to master based on these
int getStatus() {
  uint8_t rec;
  int tries;
  SPI.begin();
  boolean ARM_working, AUT_working, RA_working;
  
  // Check arm connection
  //digitalWrite(CS_ARM,LOW);  
  CS = CS_ARM;
  rec = SPI_T(0x00);
  tries = 0;
  while (rec != 0x00 && tries < MAX_TRIES) {
    delay(2);
    rec = SPI_T(0x00);  
  }
  ARM_working = (rec == 0x00);
  //digitalWrite(CS_ARM,HIGH);  
  delay(100);
  
  // Check RA connection
  //digitalWrite(CS_RA,LOW);
  CS = CS_RA;  
  rec = SPI_T(0x00);
  tries = 0;
  while (rec != 0x00 && tries < MAX_TRIES) {
    delay(2);
    rec = SPI_T(0x00);  
  }
  RA_working = (rec == 0x00);
  //digitalWrite(CS_RA,HIGH);  
  delay(100);
  
  
  // Check AUT connection
  //digitalWrite(CS_AUT,LOW);  
  CS = CS_AUT;
  rec = SPI_T(0x00);
  tries = 0;
  while (rec != 0x00 && tries < MAX_TRIES) {
    delay(2);
    rec = SPI_T(0x00);  
  }
  AUT_working = (rec == 0x00); 
  //digitalWrite(CS_AUT,HIGH);  
  
  SPI.end();
  
  // Set status integer based on which encoders are working
  int stat = 0;
  if (ARM_working && RA_working && AUT_working) {
    stat = 1;
  } else if (ARM_working && RA_working && !AUT_working) {
    stat = 2;
  } else if (ARM_working && !RA_working && AUT_working) {
    stat = 3;
  } else if (ARM_working && !RA_working && !AUT_working) {
    stat = 4;    
  } else if (!ARM_working && RA_working && AUT_working) {
    stat = 5;
  } else if (!ARM_working && RA_working && !AUT_working) {
    stat = 6;
  } else if (!ARM_working && !RA_working && AUT_working) {
    stat = 7;
  } else if (!ARM_working && !RA_working && !AUT_working) {
    stat = 8;
  }
  
  return stat;
}

uint8_t setZero(int CS_PIN) {
  uint8_t rec;
  
  // Write all 3 high incase they arent
  //digitalWrite(CS_ARM,HIGH);
  //digitalWrite(CS_AUT,HIGH);
  //digitalWrite(CS_RA,HIGH);
  
  SPI.begin();
  
  CS = CS_PIN;
  int i; 
  for (i=0; i<25; i++) {
    rec = SPI_T(0xA5); // send no op a bunch to clear encoder
    delay(2);
    if (debug) Serial.println(rec);
  }
  
  rec = SPI_T(0x70); // send set_zero command
  if (debug) Serial.println("Sent set zero command");
  delay(100);

  int tries = 0;
  
  while (rec != 0x80 && tries < 200) {
    delay(100);
    rec = SPI_T(0xA5); // send no_op
    if (debug) Serial.println(rec,HEX);
    tries++;
  }
    
  //digitalWrite(CS_PIN,HIGH);  // unselect chip
  SPI.end();    //end transmition 
  
  return rec;
}

uint8_t SPI_T (uint8_t msg)    //Repetive SPI transmit sequence
{
   uint8_t msg_temp = 0;  //vairable to hold recieved data
   digitalWrite(CS,LOW);     //select spi device
   msg_temp = SPI.transfer(msg);    //send and recieve
   digitalWrite(CS,HIGH);    //deselect spi device
   return(msg_temp);      //return recieved byte
}
