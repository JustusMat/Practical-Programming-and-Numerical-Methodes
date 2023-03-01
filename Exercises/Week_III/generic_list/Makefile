Out.txt:main.exe input.txt genlist.dll genlistfilework.cs genlistlinkednotes.dll
	mono main.exe\
		-input:input.txt\
 		1> $@ 2> log

main.exe:main.cs genlist.dll genlistfilework.dll genlistlinkednotes.dll
	mcs -target:exe -out:$@ \
		$(addprefix -reference:, $(filter %.dll, $^))\
		$(filter %.cs, $^)
		
genlist.dll:genlist.cs
	mcs -target:library -out:$@ $<
	
genlistfilework.dll:genlistfilework.cs genlist.dll
	mcs -target:library -out:$@ \
		$(addprefix -reference:, $(filter %.dll, $^)) \
		$(filter %.cs, $^)
		
genlistlinkednotes.dll:genlistlinkednotes.cs
	mcs -target:library -out:$@ $<


clean:
	$(RM) *.exe *.dll *log [Oo]u*