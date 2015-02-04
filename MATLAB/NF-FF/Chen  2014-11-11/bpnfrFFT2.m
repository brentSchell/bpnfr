function [fft2D,kx,ky] = bpnfrFFT2(Mat,xmin,ymin,dx,dy)
numX = size(Mat,2);
numY = size(Mat,1);
for n=1:numX
        kx(n)= 2*pi*(n-1-(numX-1)/2)/(numX*dx);
        
end
for m=1:numY
        ky(m)= 2*pi*(m-1-(numY-1)/2)/(numY*dy);
end
KxVec=dx*exp(1i*kx*xmin);%variables in Fourier Domain
KyVec=dy*exp(1i*ky*ymin);%variables in Fourier Domain
dummyF=fftshift(fft2(Mat));
fft2D=repmat(KyVec.', 1, size(dummyF,2)) .* repmat(KxVec,size(dummyF,1),1) .* dummyF;

end

