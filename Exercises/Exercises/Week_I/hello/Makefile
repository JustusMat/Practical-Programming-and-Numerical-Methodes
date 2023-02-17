Out.txt : main.exe				#Out.txt depends on hello.exe
	mono main.exe > Out.txt		#run hello.exe and sends output to Out.txt
	
main.exe : main.cs			#hello.exe depends on hello.cs
	mcs main.cs					#compile hello.cs into hello.exe
	
clean : 							#a phoney target no dependencies
	rm -f Out.txt main.exe		#remove secondary files