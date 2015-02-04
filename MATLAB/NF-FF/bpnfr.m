clc;
clear all;
close all;

%% contents
freq = 5e9;
c = 3e8;
lamda = c/freq;

%% current distribution
major = 15*lamda;
minor = 9*lamda;
sep = 0.5*lamda;
i = elliptical_apertrue_AUT( major, minor, sep);
%% near-field 
z_near = 5*lamda;
x_near = 40*lamda;
y_near = x_near;
step_near = 0.5*lamda;

% x_point_num = size(-x_near/2:step_near:x_near/2,2);
% y_point_num = size(-y_near/2:step_near:y_near/2,2);

r_near = null(3);
for x = -x_near/2:step_near:x_near/2
    for y = -y_near/2:step_near:y_near/2
        r_near = [r_near;[x,y,z_near]];
    end
end

f = [i';zeros(1,size(i,1))];
f = f';

