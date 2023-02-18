Out.txt : main.exe
	mono main.exe > Out.txt

main.exe:main.cs epsfunc.dll
	mcs main.cs -target:exe -out:main.exe -reference:epsfunc.dll 

	#clear my console
	clear
	
epsfunc.dll: epsfunc.cs
	mcs $^ -target:library -out:epsfunc.dll 

.PHONEY:clean
clean:
	$(RM) *.txt *.dll
	

test:
	echo test