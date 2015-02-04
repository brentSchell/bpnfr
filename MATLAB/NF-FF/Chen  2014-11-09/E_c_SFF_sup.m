function E_c_FF_sup = E_c_SFF_sup( radius_FF,dTheta,dPsi,f,freq,I )
%UNTITLED Summary of this function goes here
%   Detailed explanation goes here
%% contents
c = 3e8;
lamda = c/freq;
mu0 = 4*pi*1e-7;
e0 = 8.854187817e-12;
eta = sqrt(mu0/e0);
k = 2*pi/lamda;
len = lamda/50;
%%
r_s_far =  null(3);
% for psi = 0:dPsi:pi;
for psi = pi/4:dPsi:pi/4;
    for theta = -pi/2:dTheta:pi/2;
        r_s_far = [r_s_far;[radius_FF,theta,psi]];
    end
    
end
r_c_far = zeros(size(r_s_far,1),3);
for r_cIndex=1:size(r_s_far,1)
    x = r_s_far(r_cIndex,1)*cos(r_s_far(r_cIndex,3))*sin(r_s_far(r_cIndex,2));
    y = r_s_far(r_cIndex,1)*sin(r_s_far(r_cIndex,3))*sin(r_s_far(r_cIndex,2));
    z = r_s_far(r_cIndex,1)*cos(r_s_far(r_cIndex,2));
    r_c_far(r_cIndex,:)=[x,y,z];
end

%% rotation around x axis
thetaZ = -90*(pi/180);
cp_A_c = [1,0,0;0,cos(thetaZ),-sin(thetaZ);0,sin(thetaZ),cos(thetaZ)];
c_A_cp = cp_A_c';
%%
E_c_FF_sup = zeros(size(r_c_far,1),3);
for rIndex=1:size(r_c_far,1);
    r_c_far_test = r_c_far(rIndex,:);
    E_c_FF_mat = zeros(size(f,1),3);
    for fIndex = 1:size(f,1);
        rp_c = r_c_far_test-f(fIndex,:);
        rp_c = rp_c';
        rp_cp = cp_A_c * rp_c;
        xp = rp_cp(1);
        yp = rp_cp(2);
        zp = rp_cp(3);
        rp = sqrt(xp^2+yp^2+zp^2);
        thetap = acos(zp/rp);
        phip = atan(yp/xp);
        Ep_rp_FF = ((eta*I(fIndex)*len*cos(thetap))/(2*pi*rp^2))*(1+1/(1i*k*rp))*exp(-1i*k*rp);
        Ep_thetap_FF = (1i*eta*k*I(fIndex)*len*sin(thetap)/(4*pi*rp))*(1+1/(1i*k*rp)-1/(k^2*rp^2))*exp(-1i*k*rp);
        Ep_phip_FF = 0;
        Ep_sp_FF = [Ep_rp_FF;Ep_thetap_FF;Ep_phip_FF];
        cp_T_sp = [sin(thetap)*cos(phip),cos(thetap)*cos(phip),-sin(phip); ...
                   sin(thetap)*sin(phip),cos(thetap)*sin(phip),cos(phip); ...
                  cos(thetap),          -sin(thetap),         0];
        E_c_NF = c_A_cp*cp_T_sp*Ep_sp_FF;
        E_c_FF_mat(fIndex,:) = E_c_NF;
    end
    E_c_FF_sup_oneP = sum(E_c_FF_mat,1);
    E_c_FF_sup(rIndex,:) = E_c_FF_sup_oneP;
end

end

