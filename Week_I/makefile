Out.txt : Program.exe				#Out.txt depends on hello.exe
	mono Program.exe > Out.txt		#run hello.exe and sends output to Out.txt
	
Program.exe : Program.cs			#hello.exe depends on hello.cs
	mcs Program.cs					#compile hello.cs into hello.exe
	
clean : 							#a phoney target no dependencies
	rm -f Out.txt Program.exe		#remove secondary files