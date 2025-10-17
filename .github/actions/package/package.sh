#!/bin/bash

outputFolder=_output
artifactsFolder=_artifacts
uiFolder="$outputFolder/UI"
framework="${FRAMEWORK:=net8.0}"

rm -rf $artifactsFolder
mkdir $artifactsFolder

for runtime in _output/*
do
  name="${runtime##*/}"
  folderName="$runtime/$framework"
  ReadarrFolder="$folderName/Readarr"
  archiveName="Readarr.$BRANCH.$Readarr_VERSION.$name"

  if [[ "$name" == 'UI' ]]; then
    continue
  fi
    
  echo "Creating package for $name"

  echo "Copying UI"
  cp -r $uiFolder $ReadarrFolder
  
  echo "Setting permissions"
  find $ReadarrFolder -name "ffprobe" -exec chmod a+x {} \;
  find $ReadarrFolder -name "Readarr" -exec chmod a+x {} \;
  find $ReadarrFolder -name "Readarr.Update" -exec chmod a+x {} \;
  
  if [[ "$name" == *"osx"* ]]; then
    echo "Creating macOS package"
      
    packageName="$name-app"
    packageFolder="$outputFolder/$packageName"
      
    rm -rf $packageFolder
    mkdir $packageFolder
      
    cp -r distribution/macOS/Readarr.app $packageFolder
    mkdir -p $packageFolder/Readarr.app/Contents/MacOS
      
    echo "Copying Binaries"
    cp -r $ReadarrFolder/* $packageFolder/Readarr.app/Contents/MacOS
      
    echo "Removing Update Folder"
    rm -r $packageFolder/Readarr.app/Contents/MacOS/Readarr.Update
              
    echo "Packaging macOS app Artifact"
    (cd $packageFolder; zip -rq "../../$artifactsFolder/$archiveName-app.zip" ./Readarr.app)
  fi

  echo "Packaging Artifact"
  if [[ "$name" == *"linux"* ]] || [[ "$name" == *"osx"* ]] || [[ "$name" == *"freebsd"* ]]; then
    tar -zcf "./$artifactsFolder/$archiveName.tar.gz" -C $folderName Readarr
	fi
    
  if [[ "$name" == *"win"* ]]; then
    if [ "$RUNNER_OS" = "Windows" ]
      then
        (cd $folderName; 7z a -tzip "../../../$artifactsFolder/$archiveName.zip" ./Readarr)
      else
      (cd $folderName; zip -rq "../../../$artifactsFolder/$archiveName.zip" ./Readarr)
    fi
	fi
done
