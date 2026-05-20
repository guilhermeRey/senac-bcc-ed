#!/usr/bin/env python3
"""Gera o PDF da aula de Grafos a partir do conteudo estruturado."""

from reportlab.lib.pagesizes import A4
from reportlab.lib.units import cm, mm
from reportlab.lib.colors import HexColor, white, black
from reportlab.platypus import (
    SimpleDocTemplate, Paragraph, Spacer, Table, TableStyle,
    PageBreak, Preformatted, KeepTogether
)
from reportlab.lib.styles import ParagraphStyle, getSampleStyleSheet
from reportlab.lib.enums import TA_LEFT, TA_CENTER, TA_JUSTIFY
from reportlab.pdfbase import pdfmetrics
from reportlab.pdfbase.ttfonts import TTFont
import os

# ---------------------------------------------------------------------------
# Cores do tema
# ---------------------------------------------------------------------------
COR_PRIMARIA   = HexColor("#1a237e")   # azul escuro
COR_SECUNDARIA = HexColor("#283593")
COR_ACCENT     = HexColor("#42a5f5")   # azul claro
COR_BG_CODE    = HexColor("#f5f5f5")   # cinza claro
COR_BG_HEADER  = HexColor("#e8eaf6")   # azul muito claro
COR_BORDER     = HexColor("#c5cae9")
COR_TEXTO      = HexColor("#212121")
COR_SUBTEXTO   = HexColor("#616161")

# ---------------------------------------------------------------------------
# Estilos
# ---------------------------------------------------------------------------
styles = getSampleStyleSheet()

style_titulo = ParagraphStyle(
    "Titulo", parent=styles["Title"],
    fontSize=28, leading=34, textColor=COR_PRIMARIA,
    spaceAfter=6*mm, alignment=TA_CENTER,
    fontName="Helvetica-Bold"
)

style_subtitulo = ParagraphStyle(
    "Subtitulo", parent=styles["Normal"],
    fontSize=12, leading=16, textColor=COR_SUBTEXTO,
    spaceAfter=12*mm, alignment=TA_CENTER,
    fontName="Helvetica"
)

style_h1 = ParagraphStyle(
    "H1", parent=styles["Heading1"],
    fontSize=20, leading=26, textColor=COR_PRIMARIA,
    spaceBefore=10*mm, spaceAfter=4*mm,
    fontName="Helvetica-Bold",
    borderWidth=0, borderColor=COR_ACCENT,
    borderPadding=(0, 0, 2, 0),
)

style_h2 = ParagraphStyle(
    "H2", parent=styles["Heading2"],
    fontSize=16, leading=21, textColor=COR_SECUNDARIA,
    spaceBefore=7*mm, spaceAfter=3*mm,
    fontName="Helvetica-Bold"
)

style_h3 = ParagraphStyle(
    "H3", parent=styles["Heading3"],
    fontSize=13, leading=17, textColor=COR_SECUNDARIA,
    spaceBefore=5*mm, spaceAfter=2*mm,
    fontName="Helvetica-Bold"
)

style_body = ParagraphStyle(
    "Body", parent=styles["Normal"],
    fontSize=11, leading=15, textColor=COR_TEXTO,
    spaceAfter=3*mm, fontName="Helvetica",
    alignment=TA_JUSTIFY
)

style_bullet = ParagraphStyle(
    "Bullet", parent=style_body,
    leftIndent=8*mm, bulletIndent=3*mm,
    spaceAfter=1.5*mm,
)

style_code = ParagraphStyle(
    "Code", parent=styles["Code"],
    fontSize=9, leading=12, textColor=HexColor("#1b1b1b"),
    fontName="Courier", backColor=COR_BG_CODE,
    borderWidth=0.5, borderColor=COR_BORDER,
    borderPadding=6, spaceAfter=4*mm, spaceBefore=2*mm,
    leftIndent=4*mm, rightIndent=4*mm,
)

style_destaque = ParagraphStyle(
    "Destaque", parent=style_body,
    fontSize=11, leading=15,
    backColor=HexColor("#fff8e1"),
    borderWidth=0.5, borderColor=HexColor("#ffca28"),
    borderPadding=8, leftIndent=4*mm, rightIndent=4*mm,
    spaceAfter=4*mm, spaceBefore=2*mm,
)

style_table_header = ParagraphStyle(
    "TableHeader", parent=styles["Normal"],
    fontSize=10, leading=13, textColor=COR_PRIMARIA,
    fontName="Helvetica-Bold", alignment=TA_CENTER
)

style_table_cell = ParagraphStyle(
    "TableCell", parent=styles["Normal"],
    fontSize=10, leading=13, textColor=COR_TEXTO,
    fontName="Helvetica", alignment=TA_CENTER
)

style_table_cell_left = ParagraphStyle(
    "TableCellLeft", parent=style_table_cell,
    alignment=TA_LEFT
)

# ---------------------------------------------------------------------------
# Helpers
# ---------------------------------------------------------------------------
def sp(mm_val):
    return Spacer(1, mm_val * mm)

def heading_bar():
    """Linha horizontal decorativa."""
    t = Table([[""]],  colWidths=[17*cm], rowHeights=[1.5*mm])
    t.setStyle(TableStyle([
        ("BACKGROUND", (0, 0), (-1, -1), COR_ACCENT),
        ("LINEBELOW", (0, 0), (-1, -1), 0, white),
    ]))
    return t

def make_table(headers, rows, col_widths=None):
    """Cria uma tabela formatada."""
    header_cells = [Paragraph(h, style_table_header) for h in headers]
    data = [header_cells]
    for row in rows:
        cells = []
        for i, cell in enumerate(row):
            st = style_table_cell_left if i == 0 else style_table_cell
            cells.append(Paragraph(str(cell), st))
        data.append(cells)

    if col_widths is None:
        col_widths = [17*cm / len(headers)] * len(headers)

    t = Table(data, colWidths=col_widths, repeatRows=1)
    t.setStyle(TableStyle([
        ("BACKGROUND", (0, 0), (-1, 0), COR_BG_HEADER),
        ("TEXTCOLOR", (0, 0), (-1, 0), COR_PRIMARIA),
        ("ALIGN", (0, 0), (-1, -1), "CENTER"),
        ("VALIGN", (0, 0), (-1, -1), "MIDDLE"),
        ("GRID", (0, 0), (-1, -1), 0.5, COR_BORDER),
        ("ROWBACKGROUNDS", (0, 1), (-1, -1), [white, HexColor("#f9f9ff")]),
        ("TOPPADDING", (0, 0), (-1, -1), 4),
        ("BOTTOMPADDING", (0, 0), (-1, -1), 4),
        ("LEFTPADDING", (0, 0), (-1, -1), 6),
        ("RIGHTPADDING", (0, 0), (-1, -1), 6),
    ]))
    return t

def code_block(text):
    """Bloco de codigo formatado."""
    lines = text.strip().split("\n")
    formatted = "\n".join(lines)
    return Preformatted(formatted, style_code)

def bullet(text):
    return Paragraph(f"\u2022  {text}", style_bullet)

def bold_text(text):
    return f"<b>{text}</b>"

# ---------------------------------------------------------------------------
# Rodape
# ---------------------------------------------------------------------------
def footer(canvas, doc):
    canvas.saveState()
    canvas.setFont("Helvetica", 8)
    canvas.setFillColor(COR_SUBTEXTO)
    canvas.drawString(2*cm, 1.2*cm, "Estrutura de Dados - SENAC BCC")
    canvas.drawRightString(A4[0] - 2*cm, 1.2*cm, f"Pagina {doc.page}")
    # linha
    canvas.setStrokeColor(COR_BORDER)
    canvas.setLineWidth(0.5)
    canvas.line(2*cm, 1.5*cm, A4[0] - 2*cm, 1.5*cm)
    canvas.restoreState()

# ---------------------------------------------------------------------------
# Conteudo do PDF
# ---------------------------------------------------------------------------
def build_content():
    story = []

    # ===== CAPA =====
    story.append(sp(30))
    story.append(heading_bar())
    story.append(sp(5))
    story.append(Paragraph("Grafos (Graphs)", style_titulo))
    story.append(Paragraph("Estrutura de Dados - SENAC BCC", style_subtitulo))
    story.append(sp(5))
    story.append(heading_bar())
    story.append(sp(20))

    toc_items = [
        "1. O que e um Grafo?",
        "2. Matriz de Adjacencias",
        "3. Lista de Adjacencias",
        "4. Quando usar cada representacao?",
        "5. Busca em Profundidade (DFS)",
        "6. Busca em Largura (BFS)",
        "7. DFS vs BFS",
        "8. Casos de uso no mundo real",
    ]
    story.append(Paragraph("Conteudo", style_h2))
    for item in toc_items:
        story.append(Paragraph(item, style_body))
    story.append(PageBreak())

    # ===== 1. O QUE E UM GRAFO? =====
    story.append(Paragraph("1. O que e um Grafo?", style_h1))
    story.append(heading_bar())
    story.append(sp(3))

    story.append(Paragraph(
        "Um <b>grafo</b> e uma estrutura de dados que modela <b>relacionamentos</b> entre elementos. "
        "E composto por:", style_body
    ))
    story.append(bullet("<b>Vertices</b> (ou nos): os elementos"))
    story.append(bullet("<b>Arestas</b> (ou arcos): as conexoes entre os elementos"))
    story.append(sp(2))

    story.append(Paragraph(
        "<b>Analogia:</b> Pense em um mapa de cidades conectadas por estradas. "
        "Cada cidade e um vertice e cada estrada e uma aresta. Voce pode perguntar: "
        '"existe estrada de A para B?" ou "qual o caminho mais curto de A ate C?".',
        style_destaque
    ))

    # Tipos de grafos
    story.append(Paragraph("Tipos de Grafos", style_h3))
    story.append(make_table(
        ["Tipo", "Descricao", "Exemplo"],
        [
            ["Nao direcionado", "Arestas nos dois sentidos", "Amizade no Facebook"],
            ["Direcionado (digrafo)", "Arestas com direcao (A->B != B->A)", "Seguir no Instagram"],
            ["Ponderado", "Arestas tem peso/custo", "Distancia entre cidades"],
            ["Nao ponderado", "Todas arestas mesmo peso", "Rede de contatos"],
        ],
        [4.5*cm, 6.5*cm, 6*cm]
    ))
    story.append(sp(3))

    # Terminologia
    story.append(Paragraph("Terminologia", style_h3))
    story.append(bullet("<b>Grau</b> de um vertice: numero de arestas conectadas a ele"))
    story.append(bullet("<b>Caminho</b>: sequencia de vertices conectados por arestas"))
    story.append(bullet("<b>Ciclo</b>: caminho que comeca e termina no mesmo vertice"))
    story.append(bullet("<b>Grafo conexo</b>: existe caminho entre qualquer par de vertices"))
    story.append(bullet("<b>Adjacencia</b>: dois vertices sao adjacentes se existe aresta entre eles"))
    story.append(PageBreak())

    # ===== 2. MATRIZ DE ADJACENCIAS =====
    story.append(Paragraph("2. Matriz de Adjacencias", style_h1))
    story.append(heading_bar())
    story.append(sp(3))

    story.append(Paragraph(
        "Uma <b>matriz quadrada</b> de tamanho V x V (onde V = numero de vertices). "
        "A posicao <font face='Courier'>[i][j]</font> indica se existe aresta do vertice "
        "<font face='Courier'>i</font> para o vertice <font face='Courier'>j</font>.",
        style_body
    ))

    story.append(code_block(
        "Grafo:              Matriz de Adjacencias:\n"
        "                        0  1  2  3  4\n"
        "  0 --- 1              ---------------------\n"
        "  |     |          0 |  0  1  1  0  0\n"
        "  |     |          1 |  1  0  0  1  0\n"
        "  2 --- 3          2 |  1  0  0  1  0\n"
        "        |          3 |  0  1  1  0  1\n"
        "        4          4 |  0  0  0  1  0"
    ))

    story.append(Paragraph("Implementacao em C#", style_h3))
    story.append(code_block(
        "public class GrafoMatriz\n"
        "{\n"
        "    private int[,] matriz;\n"
        "    private int vertices;\n"
        "\n"
        "    public GrafoMatriz(int vertices)\n"
        "    {\n"
        "        this.vertices = vertices;\n"
        "        this.matriz = new int[vertices, vertices];\n"
        "    }\n"
        "\n"
        "    public void AdicionarAresta(int origem, int destino)\n"
        "    {\n"
        "        matriz[origem, destino] = 1;\n"
        "        matriz[destino, origem] = 1; // nao direcionado\n"
        "    }\n"
        "\n"
        "    public bool ExisteAresta(int origem, int destino)\n"
        "    {\n"
        "        return matriz[origem, destino] == 1;\n"
        "    }\n"
        "}"
    ))

    story.append(Paragraph("Complexidade", style_h3))
    story.append(make_table(
        ["Operacao", "Complexidade"],
        [
            ["Verificar se existe aresta", "O(1)"],
            ["Adicionar aresta", "O(1)"],
            ["Remover aresta", "O(1)"],
            ["Listar vizinhos de um vertice", "O(V)"],
            ["Espaco em memoria", "O(V^2)"],
        ],
        [9*cm, 8*cm]
    ))
    story.append(sp(3))

    story.append(bullet("<b>Vantagem:</b> Acesso direto O(1) para verificar se existe aresta"))
    story.append(bullet("<b>Desvantagem:</b> Usa O(V^2) de memoria mesmo com poucas arestas (grafo esparso)"))

    # ===== 3. LISTA DE ADJACENCIAS =====
    story.append(Paragraph("3. Lista de Adjacencias", style_h1))
    story.append(heading_bar())
    story.append(sp(3))

    story.append(Paragraph(
        "Cada vertice mantem uma <b>lista</b> com seus vizinhos. Usa um array de listas.",
        style_body
    ))

    story.append(code_block(
        "Grafo:           Lista de Adjacencias:\n"
        "\n"
        "  0 --- 1        0: [1, 2]\n"
        "  |     |        1: [0, 3]\n"
        "  |     |        2: [0, 3, 4]\n"
        "  2 --- 3        3: [1, 2, 4]\n"
        "        |        4: [2, 3]\n"
        "        4"
    ))

    story.append(Paragraph("Implementacao em C#", style_h3))
    story.append(code_block(
        "public class GrafoLista\n"
        "{\n"
        "    private List<int>[] adjacencias;\n"
        "    private int vertices;\n"
        "\n"
        "    public GrafoLista(int vertices)\n"
        "    {\n"
        "        this.vertices = vertices;\n"
        "        this.adjacencias = new List<int>[vertices];\n"
        "        for (int i = 0; i < vertices; i++)\n"
        "            adjacencias[i] = new List<int>();\n"
        "    }\n"
        "\n"
        "    public void AdicionarAresta(int origem, int destino)\n"
        "    {\n"
        "        adjacencias[origem].Add(destino);\n"
        "        adjacencias[destino].Add(origem);\n"
        "    }\n"
        "\n"
        "    public List<int> Vizinhos(int vertice)\n"
        "    {\n"
        "        return adjacencias[vertice];\n"
        "    }\n"
        "}"
    ))

    story.append(Paragraph("Complexidade", style_h3))
    story.append(make_table(
        ["Operacao", "Complexidade"],
        [
            ["Verificar se existe aresta", "O(grau do vertice)"],
            ["Adicionar aresta", "O(1)"],
            ["Remover aresta", "O(grau do vertice)"],
            ["Listar vizinhos", "O(grau do vertice)"],
            ["Espaco em memoria", "O(V + E)"],
        ],
        [9*cm, 8*cm]
    ))
    story.append(sp(3))

    story.append(bullet("<b>Vantagem:</b> Usa menos memoria para grafos esparsos: O(V + E)"))
    story.append(bullet("<b>Desvantagem:</b> Verificar se existe aresta pode custar O(grau)"))

    # ===== 4. QUANDO USAR CADA UMA =====
    story.append(KeepTogether([
        sp(5),
        Paragraph("4. Quando usar cada representacao?", style_h1),
        heading_bar(),
        sp(3),
        make_table(
            ["Criterio", "Matriz", "Lista"],
            [
                ["Grafo denso (muitas arestas)", "Melhor", "-"],
                ["Grafo esparso (poucas arestas)", "-", "Melhor"],
                ["Verificar arestas com frequencia", "Melhor", "-"],
                ["Listar vizinhos com frequencia", "-", "Melhor"],
                ["Memoria limitada", "-", "Melhor"],
            ],
            [7*cm, 5*cm, 5*cm]
        ),
    ]))

    # ===== METODO VIZINHOS =====
    story.append(PageBreak())
    story.append(Paragraph("Metodo Vizinhos: comparando as duas representacoes", style_h1))
    story.append(heading_bar())
    story.append(sp(3))

    story.append(Paragraph(
        "O metodo <font face='Courier'>Vizinhos</font> retorna todos os vertices adjacentes a um "
        "vertice dado. A implementacao e bem diferente em cada representacao, e ilustra o "
        "trade-off fundamental entre elas.",
        style_body
    ))

    story.append(Paragraph("Matriz de Adjacencias", style_h2))
    story.append(Paragraph(
        "Precisa percorrer <b>toda a linha</b> da matriz para encontrar os vizinhos. "
        "Mesmo que o vertice tenha poucos vizinhos, sempre percorre V posicoes.",
        style_body
    ))
    story.append(code_block(
        "public List<int> Vizinhos(int vertice)\n"
        "{\n"
        "    List<int> vizinhos = new List<int>();\n"
        "    for (int i = 0; i < vertices; i++)\n"
        "    {\n"
        "        if (matriz[vertice, i] == 1)\n"
        "        {\n"
        "            vizinhos.Add(i);\n"
        "        }\n"
        "    }\n"
        "    return vizinhos;\n"
        "}"
    ))
    story.append(Paragraph(
        "<b>Complexidade: O(V)</b> — sempre percorre todas as colunas, independente do numero de vizinhos.",
        style_destaque
    ))

    story.append(Paragraph("Lista de Adjacencias", style_h2))
    story.append(Paragraph(
        "Ja tem a lista pronta — basta retornar. Acesso direto, sem necessidade de busca.",
        style_body
    ))
    story.append(code_block(
        "public List<int> Vizinhos(int vertice)\n"
        "{\n"
        "    return adjacencias[vertice];\n"
        "}"
    ))
    story.append(Paragraph(
        "<b>Complexidade: O(1)</b> — retorna a referencia da lista diretamente. "
        "Iterar sobre os vizinhos custa O(grau do vertice).",
        style_destaque
    ))

    story.append(sp(3))
    story.append(Paragraph("Por que isso importa?", style_h3))
    story.append(Paragraph(
        "Os algoritmos DFS e BFS chamam <font face='Courier'>Vizinhos()</font> para <b>cada vertice</b> "
        "visitado. Essa diferenca de implementacao e o que faz a complexidade mudar:",
        style_body
    ))
    story.append(make_table(
        ["Algoritmo", "Com Matriz", "Com Lista"],
        [
            ["Vizinhos (1 vertice)", "O(V)", "O(grau)"],
            ["DFS / BFS (todos)", "O(V^2)", "O(V + E)"],
        ],
        [6*cm, 5.5*cm, 5.5*cm]
    ))

    # ===== 5. DFS =====
    story.append(Paragraph("5. Busca em Profundidade (DFS)", style_h1))
    story.append(heading_bar())
    story.append(sp(3))

    story.append(Paragraph(
        "A DFS explora o grafo indo o <b>mais fundo possivel</b> antes de retroceder. "
        "Funciona como explorar um labirinto: voce segue por um corredor ate chegar a um "
        "beco sem saida, depois volta e tenta outro caminho.",
        style_body
    ))

    story.append(Paragraph("Como funciona", style_h3))
    story.append(bullet("Comece em um vertice, marque como visitado"))
    story.append(bullet("Visite um vizinho nao visitado e repita"))
    story.append(bullet("Se todos os vizinhos ja foram visitados, retroceda (backtrack)"))
    story.append(bullet("Repita ate visitar todos os vertices alcancaveis"))
    story.append(sp(2))

    story.append(Paragraph("Visualizacao", style_h3))
    story.append(code_block(
        "Grafo:         Ordem de visitacao DFS (inicio: 0):\n"
        "\n"
        "  0 --- 1      0 -> 1 -> 3 -> 2 -> 4\n"
        "  |     |\n"
        "  |     |      Pilha (implicita na recursao):\n"
        "  2 --- 3      [0] -> [0,1] -> [0,1,3] -> [0,1,3,2] -> [0,1,3,2,4]\n"
        "        |\n"
        "        4"
    ))

    story.append(Paragraph("Implementacao recursiva em C#", style_h3))
    story.append(code_block(
        "public void DFS(int vertice, bool[] visitados)\n"
        "{\n"
        "    visitados[vertice] = true;\n"
        "    Console.Write(vertice + \" \");\n"
        "\n"
        "    foreach (int vizinho in Vizinhos(vertice))\n"
        "    {\n"
        "        if (!visitados[vizinho])\n"
        "        {\n"
        "            DFS(vizinho, visitados);\n"
        "        }\n"
        "    }\n"
        "}"
    ))

    story.append(Paragraph("Complexidade da DFS", style_h3))
    story.append(make_table(
        ["", "Matriz de Adjacencias", "Lista de Adjacencias"],
        [
            ["Tempo", "O(V^2)", "O(V + E)"],
            ["Espaco", "O(V)", "O(V)"],
        ],
        [5*cm, 6*cm, 6*cm]
    ))
    story.append(sp(3))

    story.append(KeepTogether([
        Paragraph("Aplicacoes da DFS", style_h3),
        bullet("Detectar ciclos em grafos"),
        bullet("Encontrar componentes conectados"),
        bullet("Ordenacao topologica (grafos direcionados)"),
        bullet("Resolver labirintos"),
    ]))

    # ===== 6. BFS =====
    story.append(Paragraph("6. Busca em Largura (BFS)", style_h1))
    story.append(heading_bar())
    story.append(sp(3))

    story.append(Paragraph(
        "A BFS explora o grafo <b>nivel por nivel</b>, visitando todos os vizinhos de um "
        "vertice antes de avancar. Funciona como ondas se propagando em um lago quando voce "
        "joga uma pedra.",
        style_body
    ))

    story.append(Paragraph("Como funciona", style_h3))
    story.append(bullet("Comece em um vertice, marque como visitado, coloque na <b>fila</b>"))
    story.append(bullet("Retire um vertice da fila"))
    story.append(bullet("Visite todos os vizinhos nao visitados e coloque-os na fila"))
    story.append(bullet("Repita ate a fila ficar vazia"))
    story.append(sp(2))

    story.append(Paragraph("Visualizacao", style_h3))
    story.append(code_block(
        "Grafo:         Ordem de visitacao BFS (inicio: 0):\n"
        "\n"
        "  0 --- 1      Nivel 0: 0\n"
        "  |     |      Nivel 1: 1, 2\n"
        "  |     |      Nivel 2: 3\n"
        "  2 --- 3      Nivel 3: 4\n"
        "        |\n"
        "        4      Fila: [0] -> [1,2] -> [2,3] -> [3,4] -> [4] -> []"
    ))

    story.append(Paragraph("Implementacao em C#", style_h3))
    story.append(code_block(
        "public void BFS(int inicio)\n"
        "{\n"
        "    bool[] visitados = new bool[vertices];\n"
        "    Queue<int> fila = new Queue<int>();\n"
        "\n"
        "    visitados[inicio] = true;\n"
        "    fila.Enqueue(inicio);\n"
        "\n"
        "    while (fila.Count > 0)\n"
        "    {\n"
        "        int vertice = fila.Dequeue();\n"
        "        Console.Write(vertice + \" \");\n"
        "\n"
        "        foreach (int vizinho in Vizinhos(vertice))\n"
        "        {\n"
        "            if (!visitados[vizinho])\n"
        "            {\n"
        "                visitados[vizinho] = true;\n"
        "                fila.Enqueue(vizinho);\n"
        "            }\n"
        "        }\n"
        "    }\n"
        "}"
    ))

    story.append(Paragraph("Complexidade da BFS", style_h3))
    story.append(make_table(
        ["", "Matriz de Adjacencias", "Lista de Adjacencias"],
        [
            ["Tempo", "O(V^2)", "O(V + E)"],
            ["Espaco", "O(V)", "O(V)"],
        ],
        [5*cm, 6*cm, 6*cm]
    ))
    story.append(sp(3))

    story.append(KeepTogether([
        Paragraph("Aplicacoes da BFS", style_h3),
        bullet("<b>Caminho mais curto</b> em grafos nao ponderados"),
        bullet("Encontrar componentes conectados"),
        bullet("Crawler de web (visitar paginas por nivel)"),
        bullet("Redes sociais (sugestao de amigos por grau de separacao)"),
    ]))

    # ===== 7. DFS vs BFS =====
    story.append(KeepTogether([
        Paragraph("7. DFS vs BFS", style_h1),
        heading_bar(),
        sp(3),
        make_table(
            ["Criterio", "DFS", "BFS"],
            [
                ["Estrutura auxiliar", "Pilha (recursao)", "Fila"],
                ["Estrategia", "Vai fundo, depois volta", "Explora nivel a nivel"],
                ["Caminho mais curto?", "Nao garante", "Sim (nao ponderados)"],
                ["Uso de memoria", "O(V) pior caso", "O(V) pior caso"],
                ["Melhor para...", "Ciclos, backtracking", "Caminho curto, niveis"],
            ],
            [5*cm, 6*cm, 6*cm]
        ),
        sp(5),
        Paragraph(
            "<b>Resumo:</b> Use DFS quando precisar explorar todos os caminhos ou detectar ciclos. "
            "Use BFS quando precisar do caminho mais curto ou explorar por niveis de profundidade.",
            style_destaque
        ),
    ]))

    # ===== 8. CASOS DE USO =====
    story.append(KeepTogether([
        sp(5),
        Paragraph("8. Casos de uso no mundo real", style_h1),
        heading_bar(),
        sp(3),
        make_table(
            ["Uso", "Exemplo"],
            [
                ["Redes sociais", "Amizades, seguidores, sugestoes"],
                ["Navegacao / GPS", "Cidades e estradas, caminho mais curto"],
                ["Internet", "Roteamento de pacotes entre servidores"],
                ["Compiladores", "Dependencias entre modulos"],
                ["Jogos", "Mapa de fases, pathfinding de NPCs"],
                ["Biologia", "Redes de interacao de proteinas"],
                ["Recomendacao", "Quem comprou X tambem comprou Y"],
            ],
            [5*cm, 12*cm]
        ),
    ]))

    # ===== RESUMO FINAL =====
    story.append(KeepTogether([
        sp(3),
        Paragraph("Resumo: Representacoes", style_h2),
        make_table(
            ["", "Matriz de Adj.", "Lista de Adj."],
            [
                ["Espaco", "O(V^2)", "O(V + E)"],
                ["Verificar aresta", "O(1)", "O(grau)"],
                ["Listar vizinhos", "O(V)", "O(grau)"],
                ["Adicionar aresta", "O(1)", "O(1)"],
                ["Remover aresta", "O(1)", "O(grau)"],
                ["Melhor para", "Grafos densos", "Grafos esparsos"],
            ],
            [5*cm, 6*cm, 6*cm]
        ),
    ]))

    return story

# ---------------------------------------------------------------------------
# Main
# ---------------------------------------------------------------------------
def main():
    output_path = os.path.join(os.path.dirname(__file__), "aula-grafos.pdf")

    doc = SimpleDocTemplate(
        output_path,
        pagesize=A4,
        topMargin=2*cm,
        bottomMargin=2*cm,
        leftMargin=2*cm,
        rightMargin=2*cm,
        title="Grafos - Estrutura de Dados",
        author="SENAC BCC",
    )

    story = build_content()
    doc.build(story, onFirstPage=footer, onLaterPages=footer)
    print(f"PDF gerado com sucesso: {output_path}")

if __name__ == "__main__":
    main()
