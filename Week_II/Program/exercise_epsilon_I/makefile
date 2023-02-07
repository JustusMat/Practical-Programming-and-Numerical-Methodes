Out.txt : Program.exe
	mono Program.exe > Out.txt
	
Program.exe : Program.cs
	mcs Program.cs
	
clean:
	rm -f Out.txt Program.exe
