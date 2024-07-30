using System.Collections.Generic;
using UnityEngine;

// ������
public class LineFactory : FactoryBase<LineFactory>
{
	// ���̃X�e�[�^�X
	[SerializeField]
	SO_LineStatus lineStatus;

	// ���̃X�v���C�g
	Sprite lineSprite;

	// ���̑���
	[SerializeField]
	float yScale = 0.2f;

	// �������̐�
	GameObject creatingLine = null;
	// �ʒu
	Vector3 startPos = Vector3.zero;

	// �������̃X�v���C�g�����_���[
	SpriteRenderer creatingLineSpriteRenderer = null;

	// �o��Ȃ��p�x�̏ꍇ�̃X�v���C�g
	[SerializeField]
	List<Sprite> nonClimbableSprites = new List<Sprite>();

	// ����`���Ȃ��Ƃ��̓����x
	[SerializeField]
	Color translucent;

	// ���������I������ۂɏo���G�t�F�N�g
	[SerializeField]
	GameObject createEffectPrefab;

	int number = 0;

	/// <summary>
	/// �����J�n
	/// </summary>
	/// <param name="position">�����ʒu</param>
	/// <param name="number">���̃X�e�[�^�X�̔ԍ�</param>
	public void CreateStart(Vector3 position, int number)
	{
		// �����J�n�ʒu�̐ݒ�
		startPos = position;

		// ������
		creatingLine = base.Create(position / 2);
		// �^�O�ݒ�
		creatingLine.tag = lineStatus.statuses[number].tag;
		// ���C���[�ݒ�
		creatingLine.layer = lineStatus.statuses[number].layer;
		// �X�v���C�g�����_���[�擾
		creatingLineSpriteRenderer = creatingLine.GetComponent<SpriteRenderer>();
		// �F�ݒ�
		creatingLineSpriteRenderer.sprite = lineStatus.statuses[number].sprite;
		lineSprite = lineStatus.statuses[number].sprite;
		this.number = number;
	}

	/// <summary>
	/// ������
	/// </summary>
	/// <param name="nowPosition">���݂̈ʒu</param>
	public void Creating(Vector3 nowPosition)
	{
		// �ʒu�ݒ�
		creatingLine.transform.position = (startPos + nowPosition) / 2;
		// �傫���ݒ�
		creatingLine.transform.localScale = new Vector3((startPos - nowPosition).magnitude, yScale);

		// ���̕���
		var direction = startPos - nowPosition;
		// ���̕�����y��0��艺�̏ꍇ������180�x�ς��̂ŁA�}�C�i�X�ɂ���K�v������
		if (direction.y < 0)
		{
			direction = -direction;
		}
		// �p�x�ݒ�
		creatingLine.transform.localEulerAngles = new Vector3(0, 0, Vector3.Angle(Vector3.right, direction));

		// �o�邱�Ƃ̏o���Ȃ��p�x
		if (creatingLine.transform.localEulerAngles.z > GM_Main.climbableAngle + 0.1f && creatingLine.transform.localEulerAngles.z < (180 - (GM_Main.climbableAngle + 0.1f)))
		{
			// �o��Ȃ��p�x�̏ꍇ�̃X�v���C�g�ݒ�
			creatingLineSpriteRenderer.sprite = nonClimbableSprites[number];
		}
		else
		{
			// �o���p�x�̏ꍇ�̃X�v���C�g�ݒ�
			creatingLineSpriteRenderer.sprite = lineSprite;
		}

		var isCreate = true;
		// �L�����N�^�[�����F�̐��ɏՓ˂��Ă����ꍇ�������ɂ���
		var hits = Physics2D.OverlapBoxAll(creatingLine.transform.position, creatingLine.transform.localScale, creatingLine.transform.localEulerAngles.z);
		foreach (var e in hits)
		{
			if (e.transform.gameObject != creatingLine)
			{
				if (e.transform.CompareTag("Character"))
				{
					isCreate = false;
					break;
				}
				else if (e.gameObject.CompareTag("Line") && (e.gameObject.layer == creatingLine.layer))
				{
					isCreate = false;
					break;
				}
				else if (e.gameObject.CompareTag("WarpHole"))
				{
					isCreate = false;
					break;
				}
			}
		}
		// �ŏ��T�C�Y
		var minSize = GM_Main.Instance.GetComponent<IGM_Main>().GetInstanceCharacterObjects()[0].GetComponent<ICharacter>().GetSize() / 2;
		if (minSize.x > creatingLine.transform.localScale.x)
		{
			isCreate = false;
		}
		// ����`���Ȃ��Ƃ��͔������ɂ���
		if (!isCreate)
		{
			creatingLineSpriteRenderer.color = translucent;
		}
		else
		{
			creatingLineSpriteRenderer.color = Color.white;
		}
	}

	/// <summary>
	/// ���̐����I��
	/// </summary>
	public void CreateFinish()
	{
		// �R���C�_�[�ݒ�
		creatingLine.AddComponent<BoxCollider2D>().size = new Vector2(1, 1);
		// �X�v���C�g�ݒ�
		creatingLineSpriteRenderer.sprite = lineSprite;

		// ���̍ŏ��T�C�Y
		var minSize = GM_Main.Instance.GetComponent<IGM_Main>().GetInstanceCharacterObjects()[0].GetComponent<ICharacter>().GetSize() / 2;

		var canCreate = true;
		// �ŏ��T�C�Y��菬�����ꍇ�͐������Ȃ�
		if (minSize.x > creatingLine.transform.localScale.x)
		{
			Destroy(creatingLine);
			canCreate = false;
		}
		else
		{
			// �L�����N�^�[�ɏՓ˂��Ă����ꍇ�������Ȃ�
			var hits = Physics2D.OverlapBoxAll(creatingLine.transform.position, creatingLine.transform.localScale, creatingLine.transform.localEulerAngles.z);
			foreach (var e in hits)
			{
				if (e.transform.gameObject != creatingLine)
				{
					if (e.transform.CompareTag("Character"))
					{
						Destroy(creatingLine);
						canCreate = false;
						break;
					}
					else if (e.gameObject.CompareTag("Line") && (e.gameObject.layer == creatingLine.layer))
					{
						Destroy(creatingLine);
						canCreate = false;
						break;
					}
					else if (e.gameObject.CompareTag("WarpHole"))
					{
						Destroy(creatingLine);
						canCreate = false;
						break;
					}
				}
			}
		}

		if (canCreate)
		{
			// ���Ǘ��N���X�ɓo�^
			LineManager.Instance.LineCreate(creatingLine);
			GM_Main.isPlaying = true;
		}
		// �G�t�F�N�g����
		//Instantiate(createEffectPrefab, creatingLine.transform.position, Quaternion.identity);

		// �ݒ��߂�
		creatingLine = null;
		startPos = Vector3.zero;
		creatingLineSpriteRenderer = null;
		number = 0;
	}

	/// <summary>
	/// ������
	/// </summary>
	/// <param name="startPos">�J�n�ʒu</param>
	/// <param name="endPos">�I���ʒu</param>
	/// <param name="number">�F�̔ԍ�</param>
	//public void Create(Vector3 startPos,Vector3 endPos,int number)
	//{
	//	// ������
	//	var line = base.Create((startPos + endPos) / 2);
	//	// �^�O�ݒ�
	//	line.tag = lineStatus.statuses[number].tag;
	//	// ���C���[�ݒ�
	//	line.layer = lineStatus.statuses[number].layer;
	//	// �傫���ݒ�
	//	line.transform.localScale = new Vector3((startPos - endPos).magnitude, yScale);

	//	// ���̕���
	//	var direction = startPos - endPos;
	//	// ���̕�����y��0��艺�̏ꍇ������180�x�ς��̂ŁA�}�C�i�X�ɂ���K�v������
	//	if (direction.y < 0)
	//	{
	//		direction = -direction;
	//	}
	//	// �p�x�ݒ�
	//	line.transform.localEulerAngles = new Vector3(0, 0, Vector3.Angle(Vector3.right, direction));

	//	// ���̃X�v���C�g�`��N���X�擾
	//	var lineSprite = line.GetComponent<SpriteRenderer>();
	//	// �X�v���C�g�ݒ�
	//	lineSprite.sprite = this.lineSprite;
	//	// �X�v���C�g�̐F�ݒ�
	//	lineSprite.color = lineStatus.statuses[number].color;
	//	// �R���C�_�[�ǉ�
	//	line.AddComponent<BoxCollider2D>();
	//}
}