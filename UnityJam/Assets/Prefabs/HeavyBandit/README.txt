Para crear un enemigo HeavyBandit:

- Crea el HeavyBanditPhysical en el SceneManager
- Crea el HeavyBanditPhantom en el SceneManager

- Relacionalos en el script de "partner"

- Y el player con el del player. (El Physical con el personaje fisico y el phantom con el spirit)

Para que funcione el patrullaje:

- Instancia un waypointLeft y un WaypointRight

- Añadelos al array de waypoints, primero el izquierdo, quedando a la altura del player quedando en el index 0

- Luego el derecho, tambien a la altura del enemigo quedando en el index 1

Ya tienes instanciado y funcionando el Heavybandit
