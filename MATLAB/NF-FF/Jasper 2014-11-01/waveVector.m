function [ k ] = waveVector( theta, phi, lambda )

    k = 2*pi/lambda;
    kx=k*sin(theta)*cos(phi);
    ky=k*sin(theta)*sin(phi);
    kz=k*cos(theta);
    k = [kx ky kz];

end