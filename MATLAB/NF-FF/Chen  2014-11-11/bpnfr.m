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
% theta0 = 0*pi/180;
psi0 = 45*pi/180;
% psi0 = 0*pi/180;
ellip = elliptical_apertrue_AUT( major, minor, sep,theta0, psi0,freq);
index = ellip(:,1:2);
I = ellip(:,3);
%% near-field 
z_near = 5*lamda;
x_near = 60*lamda;
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

radius_FF = 50000*lamda;
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
figure
plot(theta*180/pi,P_c_FF_norm_dB(1:1001));
% E_c_FF_sup = E_c_PFF_sup( r_c_far,f,freq );

%returns the E of NF of a plane
E_c_NF_sup = E_c_PNF_sup( r_c_near,f,freq,I );
% P_c_NF_sup = zeros(size(E_c_NF_sup,1),1);
for pIndex = 1:size(E_c_NF_sup,1);
    Ex = abs(E_c_NF_sup(pIndex,1));
    Ey = abs(E_c_NF_sup(pIndex,2));
    Ez = abs(E_c_NF_sup(pIndex,3));
    P_c_NF_sup(pIndex,1) = 0.5*(1/eta)*(Ex^2+Ey^2+Ez^2);
end
P_c_NF_sup_max = max(P_c_NF_sup);
P_c_NF_sup_norm = P_c_NF_sup/P_c_NF_sup_max;
P_c_NF_sup_norm_dB = 10*log10(P_c_NF_sup_norm);
P_c_NF_sup_norm_dB = reshape(P_c_NF_sup_norm_dB,length(-x_near/2:step_near:x_near/2),length(-y_near/2:step_near:y_near/2));
figure
surf(P_c_NF_sup_norm_dB);
Ex_NF = E_c_NF_sup(:,1);
Ey_NF = E_c_NF_sup(:,2);
Ez_NF = E_c_NF_sup(:,3);
numX = length(-x_near/2:step_near:x_near/2);
numY = length(-y_near/2:step_near:y_near/2);
dTheta = pi/1000;
dPsi = pi/1000;
E_s_FF_int = integral_EF_FF(Ex_NF,Ey_NF,r_c_near,numX,numY,dTheta,dPsi,freq);
P_s_FF_int = zeros(size(E_s_FF_int,1),1);
for pIndex = 1:size(P_s_FF_int,1);
    Etheta = abs(E_s_FF_int(pIndex,5));
    Epsi = abs(E_s_FF_int(pIndex,6));
    P_s_FF_int(pIndex) = 0.5*(1/eta)*(Etheta^2+Epsi^2);
end
P_s_FF_int_max = max(P_s_FF_int);
P_s_FF_int_norm = P_s_FF_int/P_s_FF_int_max;
P_s_FF_int_norm_dB = 10*log10(P_s_FF_int_norm);
theta = -pi/2:dTheta:pi/2;
figure
plot(theta*180/pi,P_c_FF_norm_dB(1:1001),theta*180/pi,P_s_FF_int_norm_dB(1:1001),'r');
legend('Exact Far-field','Far-field based on Exact Near-field');
% title('phi = 45 deg Cut');
xlabel('theta(deg)');
ylabel('Normolized Far-field Pattern [dB]');
% Ex_NF_2D = reshape(Ex_NF,size(-y_near/2:step_near:y_near/2,2),size(-x_near/2:step_near:x_near/2,2));
% Ey_NF_2D = reshape(Ey_NF,size(-y_near/2:step_near:y_near/2,2),size(-x_near/2:step_near:x_near/2,2));
% 
% [fx,kx,ky] = bpnfrFFT2(Ex_NF_2D,-x_near,-y_near,step_near,step_near);
% [fy,kx,ky] = bpnfrFFT2(Ey_NF_2D,-x_near,-y_near,step_near,step_near);
% 
% kx,ky to theta and psi
% theta_psi = zeros(size(-y_near/2:step_near:y_near/2,2),size(-x_near/2:step_near:x_near/2,2),2);
% for y_index=1:size(theta_psi,2)
%     for x_index=1:size(theta_psi,1)
%         psi
%         theta_psi(y_index,x_index,2) = atan(ky(y_index)/kx(x_index));
%         theta
%         theta_psi(y_index,x_index,1) = asin(kx(x_index)/(k*cos(theta_psi(y_index,x_index,2))));
%     end
% end












