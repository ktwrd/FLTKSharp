#!/bin/bash
WORKING_DIRECTORY=$(pwd)
_ensure_directory() {
    if test -d $1
    then
        rm -rf $1
    elif test -f $1
    then
        rm $1
    fi

    if [[ ! -d $1 ]]; then
        mkdir $1
    fi
}
_run_cmd() {
    if [[ $DEBUG -eq "1" ]]; then
        echo $1
    fi
    eval $1
}

_ensure_directory $WORKING_DIRECTORY/src
_ensure_directory $WORKING_DIRECTORY/out
if [[ ! -d "lib" ]]; then
    target_filename="lib_x86_64-unknown-linux-gnu.tar.gz"
    
    echo "=== Downloading binaries ($target_filename)"
    _run_cmd "wget https://github.com/MoAlyousef/cfltk/releases/latest/download/$target_filename"
    
    echo "=== Extracting binaries to ./lib/"
    _run_cmd "tar -xzvf $target_filename"

    _run_cmd "rm $target_filename"
fi

cd $WORKING_DIRECTORY/src

echo "=== Extracting binaries"
for fn in $WORKING_DIRECTORY/lib/*.a; do
    _run_cmd "cd $WORKING_DIRECTORY/lib/; ar -x $fn"
done

echo "=== Compiling to shared object"
_compile_args=""
# wayland
_compile_args="$_compile_args $(pkg-config --libs wayland-client)"
_compile_args="$_compile_args $(pkg-config --libs wayland-cursor)"
_compile_args="$_compile_args $(pkg-config --libs xkbcommon)"
_compile_args="$_compile_args $(pkg-config --libs dbus-1)"
# gtk
_compile_args="$_compile_args $(pkg-config --libs gtk+-3.0)"
# opengl
_compile_args="$_compile_args $(pkg-config --libs wayland-egl)"
_compile_args="$_compile_args $(pkg-config --libs egl)"
_compile_args="$_compile_args $(pkg-config --libs gl)"
_compile_args="$_compile_args $(pkg-config --libs glu)"
# x11
_compile_args="$_compile_args $(pkg-config --libs x11)"
_compile_args="$_compile_args $(pkg-config --libs xext)"
_compile_args="$_compile_args $(pkg-config --libs xdamage)"
_compile_args="$_compile_args $(pkg-config --libs xfixes)"
_compile_args="$_compile_args $(pkg-config --libs xtst)"
_compile_args="$_compile_args $(pkg-config --libs xinerama)"
_compile_args="$_compile_args $(pkg-config --libs xcursor)"
_compile_args="$_compile_args $(pkg-config --libs xft)"
_compile_args="$_compile_args $(pkg-config --libs pango)"
_compile_args="$_compile_args $(pkg-config --libs pangoxft)"
_compile_args="$_compile_args $(pkg-config --libs gobject-2.0)"
_compile_args="$_compile_args $(pkg-config --libs cairo)"
_compile_args="$_compile_args $(pkg-config --libs pangocairo)"
_compile_args="$_compile_args $(pkg-config --libs fontconfig)"

_run_cmd "g++ -shared *.o $_compile_args -o $WORKING_DIRECTORY/out/cfltk.so"

echo "=== Done!"
