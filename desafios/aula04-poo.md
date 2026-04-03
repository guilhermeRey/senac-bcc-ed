# POOkémon

## 🎯 Objetivo

Desenvolver um sistema simples de batalha Pokémon utilizando **Programação Orientada a Objetos (POO)** em C#.

---

## 📖 Contexto

Cada treinador possui seus Pokémon. 
Cada Pokémon possui atributos e pode atacar outro Pokémon. Alguns tipos possuem habilidades especiais.

Seu objetivo é implementar um sistema que simule batalhas entre Pokémon.

---

## 🧩 Requisitos

### 1. Classe `Pokemon`

Crie uma classe com os seguintes atributos:

- `Nome`
- `Tipo`
- `Vida`
- `Ataque`
- `Defesa`

Implemente:

- um **construtor**
- método `ExibirStatus()`
- método `Atacar(Pokemon alvo)`

---

### ⚔️ Regra de ataque

```csharp
int dano = Ataque - alvo.Defesa;
if (dano < 1) dano = 1;

alvo.Vida -= dano;
```

---

### 2. Herança (Tipos de Pokémon)

Crie pelo menos **3 subclasses**:

- `PokemonFogo`
- `PokemonAgua`
- `PokemonGrama`

Cada uma deve modificar o comportamento de ataque:

- 🔥 Fogo: causa +2 de dano
- 💧 Água: recupera 2 de vida após atacar
- 🌱 Grama: 20% de chance de ataque crítico

---

### 3. Classe `Treinador`

Atributos:

- `Nome`
- `List<Pokemon> Pokemons`

Métodos:

- `AdicionarPokemon(Pokemon p)`
- `ListarPokemons()`
- `EscolherPokemon(int indice)`

---

### 4. Simulação de batalha

No `Main`:

- Crie pelo menos **2 treinadores**
- Cada um com pelo menos **2 Pokémon**
- Escolha um Pokémon de cada treinador
- Execute uma batalha por turnos até um Pokémon ser derrotado

---

## 📺 Exemplo de saída

```text
Ash escolheu Charmander!
Misty escolheu Squirtle!

Charmander atacou Squirtle e causou 3 de dano.
Squirtle agora está com 17 de vida.

Squirtle atacou Charmander e causou 5 de dano.
Charmander agora está com 15 de vida.

...

Squirtle venceu a batalha!
```

---

## 🧠 Regras extras (opcional)

- Vantagem de tipo:
  - Fogo > Grama
  - Água > Fogo
  - Grama > Água

- Evitar vida negativa
- Exibir mensagens mais detalhadas
- Mostrar vencedor ao final

---

Tente usar uma Lista Ligada para guardar os Pokémon do treinador!