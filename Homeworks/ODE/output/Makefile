BPATH = ../build/
DPATH = ../data/
MPATH = ../

default: Example.svg OscillatorWithFriction.svg LotkaVolterra.svg

Example.svg: $(addprefix $(MPATH), Example.gpi) $(addprefix $(DPATH), example.data)
	gnuplot $<
	
OscillatorWithFriction.svg: $(addprefix $(MPATH), OscillatorWithFriction.gpi) $(addprefix $(DPATH), OWF.data)
	gnuplot $<
	
LotkaVolterra.svg: $(addprefix $(MPATH), LotkaVolterra.gpi) $(addprefix $(DPATH), LotkaVolterra.data)
	gnuplot $<