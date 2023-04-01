import numpy as np
def gaussian(x):
    return np.exp(-x**2)

def Line(n = 120):
    print('-' * n)

def main():
    a = -10e3
    b = 10e3
    n = int(10e5)
    x = np.linspace(a,b,n)
    f = gaussian(x)
    gaussian_integral = np.trapz(f,x)
    print(f"The gaussian integral with numpy's trapz method from {a} to {b} is {gaussian_integral}")
    Line()

if __name__ == "__main__":
    main()
