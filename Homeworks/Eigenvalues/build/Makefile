SPATH = ../source/
SPATH_matlib = $(addprefix $(SPATH), matlib/matrix/)

default: main.exe hydrogendiagonalisation.exe hydrogendiagonalisationexample.exe hydrogenwavefunction.exe matrixlibrary.dll

main.exe: $(addprefix $(SPATH), main.cs jacobieigenvaluealgorithm.cs) matrixlibrary.dll
	mcs -target:exe -out:$@ \
		$(filter %.cs,$^) \
		$(addprefix -reference:, $(filter %.dll, $^))


hydrogenwavefunction.exe: $(addprefix $(SPATH), hydrogenwavefunction.cs)
	mcs -target:exe -out:$@ \
		$(filter %.cs, $^) \


hydrogendiagonalisationexample.exe: $(addprefix $(SPATH), hydrogenhamiltonianclass.cs hydrogendiagonalisationexample.cs jacobieigenvaluealgorithm.cs) matrixlibrary.dll
	mcs -target:exe -out:$@ \
		$(filter %.cs ,$^) \
		$(addprefix -reference:, $(filter %.dll, $^))

hydrogendiagonalisation.exe: $(addprefix $(SPATH), hydrogenhamiltonianclass.cs hydrogendiagonalisation.cs jacobieigenvaluealgorithm.cs) matrixlibrary.dll
	mcs -target:exe -out:$@ \
		$(filter %.cs , $^) \
		$(addprefix -reference:, $(filter %.dll, $^))


matrixlibrary.dll: $(addprefix $(SPATH_matlib), matrix.cs vector.cs randommatrix.cs)
	mcs -target:library -out:$@ $^