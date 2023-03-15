BPATH = ../build/
DPATH = ../data/
MPATH = ../

default: Out.txt Hydrogen.txt rmaxvariation.svg drvariation.svg hydrogenwaves.svg

Out.txt: $(addprefix $(BPATH), main.exe)
	$^ > $@	

Hydrogen.txt: $(addprefix $(BPATH), hydrogendiagonalisationexample.exe)
	$^ > $@

rmaxvariation.svg: $(addprefix $(MPATH), rmaxvariation.gpi) $(addprefix $(DPATH), Variation_rmax.data)
	gnuplot $<

drvariation.svg: $(addprefix $(MPATH), drvariation.gpi) $(addprefix $(DPATH), Variation_dr.data)
	gnuplot $<

hydrogenwaves.svg: $(addprefix $(MPATH), hydrogenwaves.gpi) $(addprefix $(DPATH), hydrogenwaves.data)
	gnuplot $<


#Hydrogen.svg: $(addprefix $(MPATH), hydrogen.gpi)
#	gnuplot $<
