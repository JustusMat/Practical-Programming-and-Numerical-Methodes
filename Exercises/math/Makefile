Out.txt : main.exe
	mono main.exe > Out.txt
	
main.exe : main.cs
	mcs main.cs
	
clean : 							#a phoney target no dependencies
	rm -f Out.txt main.exe