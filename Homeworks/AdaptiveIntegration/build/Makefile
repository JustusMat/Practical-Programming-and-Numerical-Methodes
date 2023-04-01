SPATH = ../source/

default: main.exe erf.exe
	
main.exe: $(addprefix $(SPATH), main.cs integrator.cs)
	mcs -target:exe -out:$@ \
		$(filter %.cs, $^)
		#$(addprefix -reference:, $(filter %-dll, $^))		

erf.exe: $(addprefix $(SPATH), erf.cs integrator.cs)
	mcs -target:exe -out:$@ \
		$(filter %.cs, $^)
