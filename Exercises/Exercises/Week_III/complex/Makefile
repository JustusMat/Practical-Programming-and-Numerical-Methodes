Out.txt : main.exe
	mono main.exe > Out.txt
	
main.exe : main.cs cmath.dll
	mcs -reference:cmath.dll main.cs
	
#May needs to be modified
DIR = $(HOME)/devel/projects/PPNM/repos/Practical-Programming-and-Numerical-Methods/libraries/matlib/complex
cmath.dll : $(DIR)/cmath.cs $(DIR)/complex.cs
	mcs -target:library -out:./cmath.dll $^

.PHONEY:clean
clean:
	$(RM) *.txt *.dll  

