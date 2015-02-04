clc;
clear all;
close all;

%% contents
freq = 5e9;
c = 3e8;
lamda = c/freq;
mu0 = 4*pi*1e-7;
e0 = 8.854187817e-12;
eta = sqrt(mu0/e0);
k = 2*pi/lamda;
I0 = 1;
len = lamda/50;

%% current distribution
major = 15*lamda;
minor = 9*lamda;
sep = 0.5*lamda;
index = elliptical_apertrue_AUT( major, minor, sep);
%% near-field 
z_near = 5*lamda;
x_near = 40*lamda;
y_near = x_near;
step_near = 0.5*lamda;

% x_point_num = size(-x_near/2:step_near:x_near/2,2);
% y_point_num = size(-y_near/2:step_near:y_near/2,2);

r_c_near = null(3);
for x = -x_near/2:step_near:x_near/2
    for y = -y_near/2:step_near:y_near/2
        r_c_near = [r_c_near;[x,y,z_near]];
    end
end

f = [index';zeros(1,size(index,1))];
f = f';
%% NF C.N. 2014-11-02
%returns the E of NF of a plane
% E_c_NF_sup = E_c_PNF_sup( r_c_near,f,freq );
radius_FF = 50*lamda;
dTheta = pi/50;
dPsi = pi/50;
E_c_FF_sup = E_c_SFF_sup( radius_FF,dTheta,dPsi,f,freq);

% E_c_FF_sup = E_c_PFF_sup( r_c_far,f,freq );




















% %% new AF calculation 2014-11-01 J.T.
% 
% phi = pi/2;
% theta = 0:pi/100:pi*2;
% 
% AFlocal = @(theta) AF(theta, phi, lamda, f);
% pattern = arrayfun(AFlocal, theta);
% polar(theta, abs(pattern)/max(abs(pattern)))
% 
% %% radiation pattern infinitesimal dipole 2014-1101 J.T.
% 
% mu0 = 4*pi*1e-7;
% e0 = 8.854187817e-12;
% eta = sqrt(mu0/e0);
% I0 = 1;
% len = lamda/1000;
% k = 2*pi/lamda;
% r = 1000;
% 
% Eprime_rprimeFF = 0;
% Eprime_thetaprimeFF = 1i*eta*I0*len*sin(theta)/(4*pi*r)*exp(-1i*k*r);
% Eprime_phiprimeFF = 0;
% 
% figure
% 
% polar(theta,abs(Eprime_thetaprimeFF)/max(abs(Eprime_thetaprimeFF)))
% 

