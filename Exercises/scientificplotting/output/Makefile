DPATH = ../data/
MPATH = ../

default: Gamma.svg LnGamma.svg
	@echo finished plotting \:\)

Gamma.svg: $(addprefix $(MPATH), gamma.gpi) $(addprefix $(DPATH), gamma.data)
	gnuplot $<

LnGamma.svg: $(addprefix $(MPATH), lngamma.gpi) $(addprefix $(DPATH), lngamma.data)
	gnuplot $<