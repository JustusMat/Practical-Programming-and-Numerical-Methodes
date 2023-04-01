SPATH = ../source/
SPATH_matlib = ../source/matlib/matrix/
DPATH = ../data/
OPATH = ../output/

default: main.exe matlib.dll mclib.dll

main.exe: $(addprefix $(SPATH), main.cs) mclib.dll matlib.dll 
	mcs -target:exe -out:$@ \
		$(filter %.cs, $^) \
		$(addprefix -reference:, $(filter %.dll, $^))	
		
StratSamp.exe: $(addprefix $(SPATH), StratSamp.cs) mclib.dll matlib.dll
	mcs -target:exe -out:$@ \
		$(filter %.cs ,$^) \
		$(addprefix -reference:, $(filter %.dll, $^))

mclib.dll: $(addprefix $(SPATH), montecarlo.cs montecarlostratsamp.cs) matlib.dll
	mcs -target:library -out:$@ -r:matlib.dll $(filter %.cs, $^)

matlib.dll: $(addprefix $(SPATH_matlib), matrix.cs randommatrix.cs vector.cs)
	mcs -target:library -out:$@ $^


