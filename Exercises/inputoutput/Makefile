all: Out.txt

Out.txt: main.exe inputoutput.dll Makefile infile1.txt infile2.txt numbers.txt
	mono main.exe\
		-input:infile1.txt\
		-output:outfile.txt\
		-numbers:1,2,3,4,5,6,7 1> $@ 2> log < infile2.txt\
		
main.exe: main.cs inputoutput.dll
	mcs -target:exe -out:$@ \
		$(filter %.cs, $^) $(addprefix -reference:, $(filter %.dll, $^))

inputoutput.dll: inputoutput.cs
	mcs -target:library -out:$@ $^

numbers.txt:Makefile
	echo "-numbers:7,8,9" > $@

infile1.txt:Makefile
	echo 1 > $@
	echo 2 >> $@
	echo 3 >> $@

infile2.txt:infile1.txt
	cat $< > $@

.PHONEY:clean
clean:
	$(RM) *.exe *.dll [Oo]u* log* *.txt

test:
	echo $(RM)
