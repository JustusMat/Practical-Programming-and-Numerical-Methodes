SPATH = ../source/
SPATH_matlib = ../source/matlib/
SPATH_matlib_matrix = ../source/matlib/matrix/

default: example.exe OWF.exe matlib.dll


example.exe: $(addprefix $(SPATH), example.cs odeclass.cs) matlib.dll
	mcs -target:exe -out:$@ \
		$(filter %.cs, $^) 	\
		$(addprefix -reference:, $(filter %.dll, $^))
		
OWF.exe: $(addprefix $(SPATH), OscillatorWithFriction.cs odeclass.cs) matlib.dll
	mcs -target:exe -out:$@ \
		$(filter %.cs, $^) 	\
		$(addprefix -reference:, $(filter %.dll, $^))

matlib.dll: $(addprefix $(SPATH_matlib_matrix), vector.cs) $(addprefix $(SPATH_matlib), genlist.cs)
	@echo $^
	mcs -target:library -out:$@ $^