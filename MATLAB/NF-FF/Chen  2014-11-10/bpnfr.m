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
theta0 = 20*pi/180;
psi0 = 45*pi/180;
ellip = elliptical_apertrue_AUT( major, minor, sep,theta0, psi0,freq);
index = ellip(:,1:2);
I = ellip(:,3);
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

radius_FF = 500*lamda;
dTheta = pi/1000;
dPsi = pi/1000;
E_c_FF_sup = E_c_SFF_sup( radius_FF,dTheta,dPsi,f,freq,I);
P_c_FF = zeros(size(E_c_FF_sup,1),1);
for pIndex = 1:size(P_c_FF,1);
    Ex = abs(E_c_FF_sup(pIndex,1));
    Ey = abs(E_c_FF_sup(pIndex,2));
    Ez = abs(E_c_FF_sup(pIndex,3));
    P_c_FF(pIndex) = 0.5*(1/eta)*(Ex^2+Ey^2+Ez^2);
end
P_c_FF_max = max(P_c_FF);
P_c_FF_norm = P_c_FF/P_c_FF_max;
P_c_FF_norm_dB = 10*log10(P_c_FF_norm);
theta = -pi/2:dTheta:pi/2;
plot(theta*180/pi,P_c_FF_norm_dB(1:1001));
% E_c_FF_sup = E_c_PFF_sup( r_c_far,f,freq );

%returns the E of NF of a plane
E_c_NF_sup = E_c_PNF_sup( r_c_near,f,freq,I );
Ex_NF = E_c_NF_sup(:,1);
Ey_NF = E_c_NF_sup(:,2);
Ez_NF = E_c_NF_sup(:,3);
Ex_NF_2D = reshape(Ex_NF,size(-y_near/2:step_near:y_near/2,2),size(-x_near/2:step_near:x_near/2,2));
Ey_NF_2D = reshape(Ey_NF,size(-y_near/2:step_near:y_near/2,2),size(-x_near/2:step_near:x_near/2,2));

[fx,kx,ky] = bpnfrFFT2(Ex_NF_2D,-x_near,-y_near,step_near,step_near);
[fy,kx,ky] = bpnfrFFT2(Ey_NF_2D,-x_near,-y_near,step_near,step_near);

% kx,ky to theta and psi
theta_psi = zeros(size(-y_near/2:step_near:y_near/2,2),size(-x_near/2:step_near:x_near/2,2),2);
for y_index=1:size(theta_psi,2)
    for x_index=1:size(theta_psi,1)
        %psi
        theta_psi(y_index,x_index,2) = atan(ky(y_index)/kx(x_index));
    end
end












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

