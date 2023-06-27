SPATH = ../source/
SPATH_matlib = ../source/matlib/matrix/
DPATH = ../data/
OPATH = ../output/

default: main.exe matlib.dll mclib.dll MonteCarloConvergenceTest.exe StratifiedSamplingExample.exe

#Executables
main.exe: $(addprefix $(SPATH), main.cs) mclib.dll matlib.dll 
	mcs -target:exe -out:$@ \
		$(filter %.cs, $^) \
		$(addprefix -reference:, $(filter %.dll, $^))	
		
StratSamp.exe: $(addprefix $(SPATH), StratSamp.cs) mclib.dll matlib.dll
	mcs -target:exe -out:$@ \
		$(filter %.cs ,$^) \
		$(addprefix -reference:, $(filter %.dll, $^))
		
StratifiedSamplingExample.exe: $(addprefix $(SPATH), StratifiedSamplingExample.cs) mclib.dll matlib.dll
	mcs -target:exe -out:$@ \
		$(filter %.cs ,$^) \
		$(addprefix -reference:, $(filter %.dll, $^))
		
MonteCarloConvergenceTest.exe: $(addprefix $(SPATH), MonteCarloConvergenceTest.cs) mclib.dll matlib.dll
	mcs -target:exe -out:$@ \
		$(filter %.cs, $^) \
		$(addprefix -reference:, $(filter %.dll, $^)) 


#Libraries
mclib.dll: $(addprefix $(SPATH), montecarlo.cs montecarlostratsamp.cs MISERClass.cs) matlib.dll
	mcs -target:library -out:$@ -r:matlib.dll $(filter %.cs, $^)

matlib.dll: $(addprefix $(SPATH_matlib), matrix.cs randommatrix.cs vector.cs)
	mcs -target:library -out:$@ $^




