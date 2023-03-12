SPATH = ../source/
SPATH_matlib = $(addprefix $(SPATH), matlib/matrix/)

default: main.exe hydrogenswave.exe hydrogenswaveexample.exe matrixlibrary.dll

main.exe: $(addprefix $(SPATH), main.cs jacobieigenvaluealgorithm.cs) matrixlibrary.dll
	mcs -target:exe -out:$@ \
		$(filter %.cs,$^) \
		$(addprefix -reference:, $(filter %.dll, $^))

hydrogenswaveexample.exe: $(addprefix $(SPATH), hydrogenhamiltonianclass.cs hydrogendiagonalisationexample.cs jacobieigenvaluealgorithm.cs) matrixlibrary.dll
	mcs -target:exe -out:$@ \
		$(filter %.cs ,$^) \
		$(addprefix -reference:, $(filter %.dll, $^))

hydrogenswave.exe: $(addprefix $(SPATH), hydrogenhamiltonianclass.cs hydrogendiagonalisation.cs jacobieigenvaluealgorithm.cs) matrixlibrary.dll
	mcs -target:exe -out:$@ \
		$(filter %.cs , $^) \
		$(addprefix -reference:, $(filter %.dll, $^))


matrixlibrary.dll: $(addprefix $(SPATH_matlib), matrix.cs vector.cs randommatrix.cs)
	mcs -target:library -out:$@ $^