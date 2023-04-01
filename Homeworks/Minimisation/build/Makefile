SPATH = ../source/
SPATH_matlib = ../source/matlib/matrix/


default: main.exe higgsminimisation.exe matrixlibrary.dll minimisationlib.dll
	

#.exe
main.exe: $(addprefix $(SPATH), main.cs) matrixlibrary.dll minimisationlib.dll
	mcs -target:exe -out:$@ \
		$(filter %.cs, $^) \
		$(addprefix -reference:, $(filter %.dll, $^)) 
		
higgsminimisation.exe: $(addprefix $(SPATH), HiggsMinimisation.cs) matrixlibrary.dll minimisationlib.dll
	mcs -target:exe -out:$@ \
		$(filter %.cs, $^) \
		$(addprefix -reference:, $(filter %.dll, $^))
		
#libraries
matrixlibrary.dll: $(addprefix $(SPATH_matlib), matrix.cs vector.cs additionalmatrixmethods.cs)
	mcs -target:library -out:$@ $^

minimisationlib.dll: $(addprefix $(SPATH), minimisationclass.cs) matrixlibrary.dll
	mcs -target:library -out:$@ \
		$(filter %.cs, $^) \
		$(addprefix -reference:, $(filter %.dll, $^)) 
