function [ val ] = AF3( theta, rnew, w_n )

    sum = 0+0i;
    l = length(rnew);
    k = [2*pi*sin(theta)*cos(0), 2*pi*sin(theta)*sin(0), 2*pi*cos(theta)];
    for i = 1:l
        disp('i rnew')
        disp([i rnew(i,:)])
        disp('k')
        disp(k)
        sum = sum + w_n(i)*exp(-1j*dot(k,rnew(i,:)));
    end
    val = sum;

end

