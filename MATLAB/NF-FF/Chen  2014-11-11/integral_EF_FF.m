function [E_s_FF,f] = integral_EF_FF(Ex,Ey,points,numX,numY,dTheta,dPsi,freq)
%% contents
c = 3e8;
lamda = c/freq;
mu0 = 4*pi*1e-7;
e0 = 8.854187817e-12;
eta = sqrt(mu0/e0);
k = 2*pi/lamda;
len = lamda/50;
%%
% psi = 0:dPsi:pi;
psi = pi/4:dPsi:pi/4;
% psi = 0:dPsi:0;
theta = -pi/2:dTheta:pi/2;
points = reshape(points,numY,numX,3);
Ex = reshape(Ex,numY,numX);
Ey = reshape(Ey,numY,numX);
% x_mat = points(:,:,1);
% y_mat = points(:,:,2);
f_index = 1;
f = zeros(length(psi)*length(theta),4);
for psi = psi;
    for theta = theta;
        kx = k*sin(theta)*cos(psi);
        ky = k*sin(theta)*sin(psi);
        f(f_index,1) = theta;
        f(f_index,2) = psi;
        fx = 0;
        fy = 0;
        for yi = 1:size(points,1)-1;
            fxx = 0;
            fyx = 0;
            for xi = 1:size(points,2)-1;
                x = points(yi,xi,1);
                y = points(yi,xi,2);
                dx = points(yi,xi+1,1)-x;
                fxx = fxx+Ex(yi,xi)*exp(1i*(kx*x+ky*y))*dx;
                fyx = fyx+Ey(yi,xi)*exp(1i*(kx*x+ky*y))*dx;
            end
            dy = points(yi+1,xi,2)-y;
            fx = fx + fxx*dy;
            fy = fy + fyx*dy;
        end
        f(f_index,3) = fx;
        f(f_index,4) = fy;
        f_index = f_index+1;
    end
end
%%
for e_index = 1:f_index-1
    theta = f(e_index,1);
    E_s_FF(e_index,1) = theta;
    psi = f(e_index,2);
    E_s_FF(e_index,2) = psi;
    fx = f(e_index,3);
    E_s_FF(e_index,3) = fx;
    fy = f(e_index,4);
    E_s_FF(e_index,4) = fy;
    E_theta_FF = fx*cos(psi)+fy*sin(psi);
    E_s_FF(e_index,5) = E_theta_FF;
    E_psi_FF = cos(theta)*(-fx*sin(psi)+fy*cos(psi));
    E_s_FF(e_index,6) = E_psi_FF;
end




end

