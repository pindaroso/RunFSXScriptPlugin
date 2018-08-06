all:
	@cd RunFSXScriptPlugin && rm -rf RunFSXScriptPlugin.rhp | echo "Skipping..."
	@cd RunFSXScriptPlugin && cp -r bin/Release RunFSXScriptPlugin.rhp
	@cd RunFSXScriptPlugin && zip -r RunFSXScriptPlugin.macrhi RunFSXScriptPlugin.rhp
	@cd RunFSXScriptPlugin && rm -rf RunFSXScriptPlugin.rhp
