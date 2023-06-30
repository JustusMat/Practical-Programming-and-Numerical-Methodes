# Exam
My study number ends with the digits 59, so consequently I pulled the exam 59 modulo 26 = 7 which is "Yet another cubic sub-spline".


## Critical assessment of my exam project
For the critical assessment of my exam project, I follow the scoring criteria of the homework.

The main part A) of the exam results in the implementation of a new cubic sub-spline. 
The biggest challenge in this part was to determine the formulas for the coefficients of 
the second-degree polynomial based on three points (see Figure derivatives.pdf) 
and to find the subsequent coefficients of the cubic sub-splines using the given criteria. 
Since neither the former nor the latter is included in the lecture notes, I had to derive and implement them properly on my own. 
Furthermore, the binary search algorithm was implemented. Based on this, I give myself 6/6 points for the main part.

Further points are achieved in the B) part, which for me is the extra implementation of the derivative and the anti-derivative. 
These were also implemented successfully and therefore 3/3 points are awarded here. 
However, a marginal note on the derivative must be made. As the cubic sub-spline with the given conditions is a $C^1$ differentiable 
function just like the Akima sub-spline, it is itself continuous as well as its first derivative. 
However, the second derivative is not necessarily continuous, which explains why the derivative is only piecewise smooth. 

Finally, I give myself 1/1 point in the C part, representing the additional extra part. 
The justification lies in the further implementation and comparison of an Akima sub-spline which in fact is a separate exam project.

In summary, I give myself 10/10 points for the exam project.