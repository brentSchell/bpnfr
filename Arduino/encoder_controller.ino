
#include <SPI.h>
boolean debug = true;
int CS_ARM = 3;
int CS_RA = 4;
int CS_AUT = 5;
int CS = 3; 
int MAX_TRIES = 10;
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
     Serial.print(command,DEC);
     Serial.print(","); 
     Serial.println(stat,DEC); 
     CS = -1;
   } else {
     CS = -1;  
   }
   
   if (CS != -1) {
     float pos_avg = 0;
     int pos = 0;
     int fail_count = 0;
     for (int i=0; i<10 && fail_count<MAX_TRIES; i++) { // get 10 measurements, or 10 fails then stop
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
       pos_avg /= 10.0;
     }
     if (debug) {
       Serial.print("Average: " );
       Serial.println(pos_avg,DEC); 
     }
     
     // Send response back to master
     Serial.print(command,DEC);
     Serial.print(","); 
     Serial.println(pos_avg,DEC); 
     double stop_time = micros() - start_time;
     Serial.print("Time to get pos: ");
     Serial.println(stop_time,DEC); 
     
   }
 
}

int getPosition() { // gets position of currently assigned Encoder, stored 2 bytes
   uint8_t recieved = 0xA5;
   int ABSposition = -1;

   SPI.begin();    //start transmition
   digitalWrite(CS,LOW);
   
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
     fail_count++;
     delay(2);    //wait a bit
   }

   temp[0] = SPI_T(0x00);    //Recieve MSB
   temp[1] = SPI_T(0x00);    // recieve LSB   
   
   digitalWrite(CS,HIGH);  //just to make sure   
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

uint8_t SPI_T (uint8_t msg)    //Repetive SPI transmit sequence
{
   uint8_t msg_temp = 0;  //vairable to hold recieved data
   digitalWrite(CS,LOW);     //select spi device
   msg_temp = SPI.transfer(msg);    //send and recieve
   digitalWrite(CS,HIGH);    //deselect spi device
   return(msg_temp);      //return recieved byte
}

// TODO: check all 3 connections, give status to master based on these
int getStatus() {
  return 1;
}

