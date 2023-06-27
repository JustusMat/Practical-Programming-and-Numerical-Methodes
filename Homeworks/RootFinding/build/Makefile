SPATH = ../source/
SPATH_matlib = ../source/matlib/
SPATH_matlib_matrix = ../source/matlib/matrix/



default: main.exe matrixlibrary.dll odelibrary.dll rootfindinglib.dll BSHydrogen.exe InvestigateConvergence.exe


#Executables
main.exe: $(addprefix $(SPATH), main.cs) matrixlibrary.dll rootfindinglib.dll
	mcs -target:exe -out:$@ \
		$(filter %.cs, $^) \
		$(addprefix -reference:, $(filter %.dll, $^))

BSHydrogen.exe: $(addprefix $(SPATH), BSHydrogen.cs) matrixlibrary.dll rootfindinglib.dll odelibrary.dll
	mcs -target:exe -out:$@ \
		$(filter %.cs, $^) \
		$(addprefix -reference:, $(filter %.dll, $^))

InvestigateConvergence.exe: $(addprefix $(SPATH), InvestigateConvergence.cs) matrixlibrary.dll rootfindinglib.dll odelibrary.dll
	mcs -target:exe -out:$@ \
		$(filter %.cs, $^) \
		$(addprefix -reference:, $(filter %.dll, $^))

#Libraries
matrixlibrary.dll: $(addprefix $(SPATH_matlib_matrix), matrix.cs vector.cs)
	mcs -target:library -out:$@ $^


odelibrary.dll: $(addprefix $(SPATH_matlib), genlist.cs odeclass.cs) matrixlibrary.dll
	mcs -target:library -out:$@\
		$(filter %.cs, $^) \
		$(addprefix -reference:, $(filter %.dll, $^))

rootfindinglib.dll: $(addprefix $(SPATH), rootfindingclass.cs QRGramSchmidt.cs) matrixlibrary.dll
	mcs -target:library -out:$@ \
		$(filter %.cs, $^) \
		$(addprefix -reference:, $(filter %.dll, $^))

