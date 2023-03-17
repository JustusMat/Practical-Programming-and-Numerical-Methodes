SPATH = ../source/


default: main.exe main.exe

main.exe: $(addprefix $(SPATH), main.cs linearinterpolation.cs quadraticinterpolation.cs)
	mcs -target:exe -out:$@ \
		$(filter %.cs, $^)
		$(addprefix -reference:, $(filter %.dll, $^))
