SPATH = ../source/
BPATH = ../build/
DPATH = ../data/
OPATH = ../output/
MPATH = ../

default: Out.txt gData.svg dgdxData.svg d2gdx2Data.svg integralgData.svg
	
Out.txt: $(addprefix $(BPATH), main.exe)
	mono $^ > $@ 
	
gData.svg: $(addprefix $(MPATH), gDataPlot.gpi) $(addprefix $(DPATH), gData.data)
	gnuplot $<

dgdxData.svg: $(addprefix $(MPATH), dgdxDataPlot.gpi) $(addprefix $(DPATH), gDataDerivative.data)
	gnuplot $<

d2gdx2Data.svg: $(addprefix $(MPATH), d2gdx2DataPlot.gpi) $(addprefix $(DPATH), gDataDerivative2.data)
	gnuplot $<

integralgData.svg: $(addprefix $(MPATH), integralgDataPlot.gpi) $(addprefix $(DPATH), gDataAntiDerivative.data)
	gnuplot $<