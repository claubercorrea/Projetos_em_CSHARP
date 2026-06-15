<!--- MEUS PROJETOS WEB

PROJETOS EM DESENVOLVIMENTO


Baixe e instale a extensão de linha de comando do Git. Após o download e a instalação, configure o Git LFS para sua conta de usuário executando o seguinte comando:

git lfs instalar
Você só precisa executar este comando uma vez por conta de usuário.

Em cada repositório Git onde você deseja usar o Git LFS, selecione os tipos de arquivo que você gostaria que o Git LFS gerenciasse (ou edite diretamente seus arquivos .gitattributes). Você pode configurar extensões de arquivo adicionais a qualquer momento.

git lfs track "*.psd"
Agora, certifique-se de que o arquivo .gitattributes esteja sendo rastreado:

git add .gitattributes
Note que definir os tipos de arquivo que o Git LFS deve rastrear não converterá, por si só, arquivos preexistentes para o formato Git LFS, como arquivos em outros branches ou no seu histórico de commits. Para isso, use o comando `git lfs migrate(1)` , que possui uma variedade de opções projetadas para atender a diversos casos de uso.

Não há um terceiro passo. Basta fazer o commit e o push como você normalmente faria; por exemplo, se o nome da sua branch atual for main:

git adicionar arquivo.psd
git commit -m "Adicionar arquivo de projeto"
git push origin main --->



PROJETOS C#
