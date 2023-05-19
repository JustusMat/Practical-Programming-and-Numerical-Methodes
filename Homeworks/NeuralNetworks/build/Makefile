SPATH = ../source/
SPATH_matlib_matrix = ../source/matlib/matrix/
SPATH_matlib = ../source/matlib/

default: main.exe matlib.dll minimisationlib.dll NNlib.dll
	
main.exe: $(addprefix $(SPATH), main.cs) matlib.dll minimisationlib.dll NNlib.dll
	mcs -target:exe -out:$@ \
		$(filter %.cs, $^) \
		$(addprefix -reference:, $(filter %.dll, $^))		


#Libraries
matlib.dll: $(addprefix $(SPATH_matlib_matrix), vector.cs matrix.cs additionalmatrixmethods.cs additionalvectormethods.cs) 
	mcs -target:library -out:$@ $^

minimisationlib.dll: $(addprefix $(SPATH_matlib), minimisationclass.cs) matlib.dll
	mcs -target:library -out:$@ \
		$(filter %.cs, $^) \
		$(addprefix -reference:, $(filter %.dll, $^))
		
NNlib.dll: $(addprefix $(SPATH), SimpleNeuralNetwork.cs SimpleNeuralNetworkDerivatives.cs) matlib.dll minimisationlib.dll
	mcs -target:library -out:$@ \
		$(filter %.cs, $^) \
		$(addprefix -reference:, $(filter %.dll, $^))