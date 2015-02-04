clear all
close all
clc

% frequency
freq = 5e9;
% lambda
l = 3e8/freq;

% alpha, beta, gamma, coordinate rotation
a = 0;
b = 3*pi/2;
g = 3*pi/2;

%fVectorC = [l/2  l/2  0]';
%fVectorC = [0 0  0]';
cprimeAc = [ cos(g)*cos(a) - sin(g)*cos(b)*sin(a) , cos(g)*sin(a) + sin(g)*cos(b)*cos(a) , sin(g)*sin(b) ; ... 
  -sin(g)*cos(a) - cos(g)*cos(b)*sin(a), -sin(g)*sin(a) + cos(g)*cos(b)*cos(a), cos(g)*sin(b); ...
  sin(b)*sin(a), -sin(b)*cos(a), cos(b) ];
%rVectorC = [ 0 0 0.3 ]';
%rVectorC = [ 0 0 1000*l ]';
rprimeVectorC = [rVectorC - fVectorC];
rprimeVectorCprime = cprimeAc * rprimeVectorC;

xprime = rprimeVectorCprime(1);
yprime = rprimeVectorCprime(2);
zprime = rprimeVectorCprime(3);

rprimeSpherical = sqrt(xprime^2+yprime^2+zprime^2);
thetaprimeSpherical = acos(zprime/rprimeSpherical);
phiprimeSpherical = atan(yprime/xprime);

% fields

mu0 = 4*pi*1e-7;
e0 = 8.854187817e-12;
eta = sqrt(mu0/e0);
I0 = 1;
length = l/1000;
k = 2*pi/l;

% near-fields

Eprime_rprime = eta*I0*length*cos(thetaprimeSpherical)/(2*pi*rprimeSpherical^2)*(1+1/(1j*k*rprimeSpherical))*exp(-1i*k*rprimeSpherical);
Eprime_thetaprime = 1i*eta*I0*length*sin(thetaprimeSpherical)/(4*pi*rprimeSpherical)*(1+1/(1j*k*rprimeSpherical)-1/(k^2*rprimeSpherical^2))*exp(-1i*k*rprimeSpherical);
Eprime_phiprime = 0;

% far-fields

Eprime_rprimeFF = 0;
Eprime_thetaprimeFF = 1i*eta*I0*length*sin(thetaprimeSpherical)/(4*pi*rprimeSpherical)*exp(-1i*k*rprimeSpherical);
Eprime_phiprimeFF = 0;

%

EprimeSprime = [ Eprime_rprime; Eprime_thetaprime; Eprime_phiprime ];

cprimeTsprime = [ sin(thetaprimeSpherical)*cos(phiprimeSpherical), cos(thetaprimeSpherical)*cos(phiprimeSpherical), -sin(phiprimeSpherical); ...
                  sin(thetaprimeSpherical)*sin(phiprimeSpherical), cos(thetaprimeSpherical)*sin(phiprimeSpherical), cos(phiprimeSpherical); ...
                  cos(thetaprimeSpherical), -sin(thetaprimeSpherical), 0 ];
              
Ec = (cprimeAc')*(cprimeTsprime*EprimeSprime);

