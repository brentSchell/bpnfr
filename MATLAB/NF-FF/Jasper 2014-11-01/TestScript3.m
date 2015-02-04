clear all
close all
clc

% end-fire
%theta_0 = 0 degree
% N = 10
% d = lambda/4
%Fig 6.8
% beta = - k*d

k = 2*pi;
d = 1/4;
beta = -k*d;
n = 1:1:10;
phi = @(theta) k*d*cos(theta)+beta;
AF = @(theta) sum(exp(1i.*(n-1).*(phi(theta))));
theta = 0:pi/100:2*pi;
pattern = arrayfun(AF,theta);
polar(theta,abs(pattern))
% Array Factor
N = 10;
AF2 = @(theta) sin(N/2*phi(theta))/sin(phi(theta)/2);
pattern2 = arrayfun(AF2, theta);
hold on 
polar(theta,abs(pattern2),'ro')

r1 = [ 0 0 0 ] ;
r2 = [ 0 0 d ] ;
r3 = [ 0 0 2*d ] ;
r4 = [ 0 0 3*d ] ;
r5 = [ 0 0 4*d ] ;
r6 = [ 0 0 5*d ] ;
r7 = [ 0 0 6*d ] ;
r8 = [ 0 0 7*d ] ;
r9 = [ 0 0 8*d ] ;
r10 = [ 0 0 9*d ] ;


n_weights = 0:1:9;
w_n = exp(1j*2*pi*n_weights*d);
r = [ r1' r2' r3' r4' r5' r6' r7' r8' r9' r10' ];
% kxwavevector = @(theta) 2*pi*sin(theta)*cos(0);
% kywavevector = @(theta) 2*pi*sin(theta)*sin(0);
% kzwavevector = @(theta) 2*pi*cos(theta);
rnew = r';
AF3theta = @(theta) AF3(theta,rnew,w_n);
pattern3 = arrayfun(AF3theta, theta);
polar(theta,abs(pattern3),'.k')

