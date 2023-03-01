SPATH = ../source/
SPATH_matlib = $(addprefix $(SPATH), matlib/matrix/)

default: main.exe Benchmark.exe matrixlibrary.dll

main.exe: $(addprefix $(SPATH), main.cs QRGramSchmidt.cs randommatrixclass.cs) matrixlibrary.dll
	mcs -target:exe -out:$@ \
		$(filter %.cs,$^) \
		$(addprefix -reference:, $(filter %.dll, $^))	
		
Benchmark.exe: $(addprefix $(SPATH), BenchmarkQRDecomposition.cs QRGramSchmidt.cs randommatrixclass.cs) matrixlibrary.dll
	mcs -target:exe -out:$@ \
		$(filter %.cs,$^) \
		$(addprefix -reference:, $(filter %.dll, $^))	

matrixlibrary.dll: $(addprefix $(SPATH_matlib), matrix.cs vector.cs)
	mcs -target:library -out:$@ $^