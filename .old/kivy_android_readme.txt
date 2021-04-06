1. sudo apt update
2. sudo apt upgrade

# https://kivy.org/doc/stable/installation/installation-linux.html
3. sudo apt-get install -y \
    python3-pip \
    build-essential \
    git \
    python3 \
    python3-dev \
    cython

4. pip3 install kivy kivy-examples cython

# https://kivy.org/doc/stable/guide/packaging-android.html
5. git clone https://github.com/kivy/buildozer.git && cd buildozer && sudo python3 setup.py install

6. cd <ProjectDir>
7. buildozer android debug
8. buildozer android release
9. buildozer android debug deploy run
10. buildozer android release deploy run