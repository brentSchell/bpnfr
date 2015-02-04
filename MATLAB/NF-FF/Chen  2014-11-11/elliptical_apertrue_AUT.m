function output = elliptical_apertrue_AUT( major, minor, sep, theta0, psi0,freq)
% sep is the speration of two dipole
% this function will return a 2d matrix every roll will be [x,y] 
%% 
c = 3e8;
lamda = c/freq;
mu0 = 4*pi*1e-7;
e0 = 8.854187817e-12;
eta = sqrt(mu0/e0);
k = 2*pi/lamda;
I0 = 1;
len = lamda/50;
t=0:pi/10:2*pi;
%%

x=(major/2)*cos(t);
y=(minor/2)*sin(t);
%points to be checked
output = null(3);
for xq=-major/2:sep:major/2
    for yq=-minor/2:sep:minor/2
        if(inpolygon(xq,yq,x,y))
            I = I0*exp(1i*(-2*pi*xq*sin(theta0)*cos(psi0)/lamda-2*pi*yq*sin(theta0)*sin(psi0)/lamda));
            output = [output;[xq,yq,I]];
        end
    end
end
% scatter(output(:,1),output(:,2),abs(output(:,3)));
quiver(output(:,1),output(:,2),real(output(:,3)),imag(output(:,3)),0.1);
end

