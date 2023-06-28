SPATH = ../source/


default: main.exe interpolationlib.dll

main.exe: $(addprefix $(SPATH), main.cs) interpolationlib.dll
	mcs -target:exe -out:$@ \
		$(filter %.cs, $^)	\
		$(addprefix -reference:, $(filter %.dll, $^))

#Libraries 
interpolationlib.dll: $(addprefix $(SPATH), linearinterpolation.cs quadraticinterpolation.cs cubicinterpolation.cs)
	mcs -target:library -out:$@ $^