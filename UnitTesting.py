#!/usr/bin/env python3

import unittest
import requests
import threading
import time
import os

def start_dotnet():
    os.system("dotnet run")

class TestGetEndpoints(unittest.TestCase):
    def test_index(self):
        # Hacer una test simple para asegurarse que todo funcione correctamente
        # esto es una simple cadena de GETs
        urls_to_test = [
            "Index",
            "Vacante/Index",
            "Vacante/Crear",
            "Prospecto/Index",
            "Prospecto/Crear",
            "Entrevista/Index",
            "Entrevista/Crear"
        ]

        for test_url in urls_to_test:
            final_url = "http://localhost:5000/{}".format(test_url)
            r = requests.get(url=final_url)
            self.assertTrue(r.status_code == 200)

if __name__ == '__main__':
    os.chdir(os.getcwd() + "/WebServer")
    print(os.getcwd())

    # Construir el proyecto primero
    os.system("dotnet build")

    x = threading.Thread(target=start_dotnet, daemon=True)
    x.start()

    # Hay que esperar 10 segundos a que asp.net pueda iniciarse correctamente
    time.sleep(10)
    unittest.main()
    x.join()