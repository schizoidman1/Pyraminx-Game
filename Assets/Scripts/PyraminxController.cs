using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PyraminxController : MonoBehaviour
{
    public float rotationSpeed = 50f;  // Velocidade de rotação das peças
    private int selectedFace = -1;     // Índice da face selecionada

    // Centro de cada face
    public Vector3[] faceCenters;      // Posições centrais de cada face

    // Eixos de rotação para cada face (depende da orientação do Pyraminx no espaço)
    public Vector3[] faceRotationAxes; // Eixos de rotação para cada face

    // Referências para as peças que compõem cada face
    public List<PyraminxPiece>[] facePieces;  // Um array de listas, onde cada lista contém as peças de uma face

    void Update()
    {
        // Selecionar uma face com as teclas Q, W, E, R
        if (Input.GetKeyDown(KeyCode.Q)) {
            selectedFace = 0; // Exemplo: Face 1
            Debug.Log("Face 1 selecionada");
        } else if (Input.GetKeyDown(KeyCode.W)) {
            selectedFace = 1; // Exemplo: Face 2
            Debug.Log("Face 2 selecionada");
        } else if (Input.GetKeyDown(KeyCode.E)) {
            selectedFace = 2; // Exemplo: Face 3
            Debug.Log("Face 3 selecionada");
        } else if (Input.GetKeyDown(KeyCode.R)) {
            selectedFace = 3; // Exemplo: Face 4
            Debug.Log("Face 4 selecionada");
        }

        // Se uma face estiver selecionada, rotacioná-la
        if (selectedFace != -1)
        {
            if (Input.GetKey(KeyCode.A)) {
                RotateFace(selectedFace, -rotationSpeed * Time.deltaTime); // Rotação anti-horária
            } else if (Input.GetKey(KeyCode.D)) {
                RotateFace(selectedFace, rotationSpeed * Time.deltaTime);  // Rotação horária
            }
        }
    }

    // Função para rotacionar a face selecionada
    void RotateFace(int faceIndex, float angle)
    {
        Vector3 rotationAxis = faceRotationAxes[faceIndex];  // Eixo de rotação da face
        Vector3 faceCenter = faceCenters[faceIndex];         // Centro da face (ponto de rotação)

        // Aplicando todas as três formas de rotação

        // 1. Rotacionando com RotateAround em torno do centro da face
        foreach (PyraminxPiece piece in facePieces[faceIndex])
        {
            // Rotaciona a peça ao redor do centro da face
            piece.transform.RotateAround(faceCenter, rotationAxis, angle);

            // 2. Rotacionando a peça localmente ao redor de seu próprio eixo
            piece.transform.Rotate(Vector3.up * angle);  // Exemplo de rotação ao redor do próprio eixo

            // 3. Aplicando rotação com Quaternion para uma rotação suave e precisa
            Quaternion rotation = Quaternion.AngleAxis(angle, rotationAxis);
            piece.transform.rotation = rotation * piece.transform.rotation;
        }
    }
}

