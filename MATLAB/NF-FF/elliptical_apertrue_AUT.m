function output = elliptical_apertrue_AUT( major, minor, sep)
% sep is the speration of two dipole
% this function will return a 2d matrix every roll will be [x,y] 
% assume it is on z=0 plane and meg=1 
t=0:pi/10:2*pi;
x=(major/2)*cos(t);
y=(minor/2)*sin(t);
%points to be checked
output = null(2);
for xq=-major/2:sep:major/2
    for yq=-minor/2:sep:minor/2
        if(inpolygon(xq,yq,x,y))
            output = [output;[xq,yq]];
        end
    end
end
end

