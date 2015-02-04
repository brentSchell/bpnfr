function [ val ] = AF( theta, phi, lambda, f )

    sum = 0+0i;
    l = length(f);
    k = waveVector(theta,phi,lambda);
    for i = 1:l
        sum = sum + exp(-1j*dot(k,f(i,:)));
    end
    val = sum;

end