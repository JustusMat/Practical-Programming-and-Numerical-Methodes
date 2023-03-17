SPATH = ../source/
SPATH_matlib = $(addprefix $(SPATH), matlib/matrix/)

default: main.exe

test.exe: $(addprefix $(SPATH), test.cs QRGramSchmidt.cs) matrixlibrary.dll
	mcs -target:exe -out:$@ \
		$(filter %.cs, $^) \
		$(addprefix -reference:, $(filter %.dll, $^))
		
main.exe: $(addprefix $(SPATH), main.cs QRGramSchmidt.cs leastsq.cs loaddata.cs) matrixlibrary.dll
	mcs -target:exe -out:$@ \
		$(filter %.cs, $^) \
		$(addprefix -reference:, $(filter %.dll, $^))

matrixlibrary.dll: $(addprefix $(SPATH_matlib), matrix.cs vector.cs randommatrixclass.cs)
	mcs -target:library -out:$@ $^