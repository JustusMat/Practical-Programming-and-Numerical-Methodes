SPATH = ../source/


default: main.exe CubicSubSpline.dll
	@echo Finished build

#Executables
main.exe: $(addprefix $(SPATH), main.cs ) CubicSubSpline.dll
	mcs -target:exe -out:$@ \
		$(filter %.cs, $^) \
		$(addprefix -reference:, $(filter %.dll, $^))	


#Libraries
CubicSubSpline.dll: $(addprefix $(SPATH), CubicSubSpline.cs) 
	mcs -target:library -out:$@ $^
