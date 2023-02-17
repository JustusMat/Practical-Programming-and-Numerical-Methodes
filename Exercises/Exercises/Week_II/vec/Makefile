#Save output in Out.txt
Out.txt : main.exe
	mono main.exe > Out.txt
	
#compile the vec.cs into a library
vec.dll : vec.cs
	mcs $^ -target:library -out:vec.dll
	
#Create the main executable illustrating my implementation
main.exe: main.cs vec.dll
	mcs main.cs -target:exe -out:main.exe -reference:vec.dll 
	
.PHONEY:clean

clean:
	$(RM) *.exe *.dll [Oo]u* log*