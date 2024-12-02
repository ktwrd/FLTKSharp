#!/bin/bash

dnf group install development-tools c-development

dnf install -y \
    wayland-devel \
    wayland-protocols-devel \
    egl-wayland-devel \
    libxkbcommon-devel \
    pango-devel \
    cairo-devel \
    libX11-devel \
    libXinerama-devel \
    libXdamage-devel \
    libXfixes-devel \
    libwayland-client \
    libwayland-cursor \
    egl-x11 \
    egl-wayland-devel \
    libwayland-egl \
    fontconfig-devel \
    gtk3-devel \
    dbus-devel \
    mesa-libGLU-devel \
    libdecor-devel \
    glew-devel \
    cmake \
    autoconf
