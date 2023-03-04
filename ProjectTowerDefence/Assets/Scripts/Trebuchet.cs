using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class Trebuchet : Tower
    {
        private static readonly float shootingAnimationLength = 9.3f;
        private static readonly float bulletAnimationDelay = 0.1f;
        private Animator animator;
        private GameObject machina;
        [SerializeField] public float rotationStepPerSecond;

        private bool awaitingBulletFire = false;


        protected override void Start()
        {
            base.Start();
            animator = transform.Find("Trebuchet.fbx").GetComponent<Animator>();

            //gimnastyka tutaj jest spowodowana faktem że animacje nie były zrobione szkieletami tylko zmiana rotacji poszczególnych części terbusza
            //a unity nie pozwala ingerować w pliki fbx dlatego skryptem zmieniam hierarchie. 
            GameObject treb = transform.Find("Trebuchet.fbx").gameObject;
            machina = treb.transform.Find("machina").gameObject;
            GameObject wyrzutnia = treb.transform.Find("wyrzutnia").gameObject;
            wyrzutnia.transform.SetParent(machina.transform);

            animator.speed = shootingAnimationLength / maxCooldown;

        }

        protected override void Update()
        {
            base.Update();
            var currentAnimationState = animator.GetCurrentAnimatorStateInfo(0);

            machina.transform.Rotate(new Vector3(0,2*Time.deltaTime,0));
            //jeżeli nie ma rpzeciwnika to rotuje sie ... nie rotuje sie 
            if(currEnemieToHit){
                Vector3 deltaPosition = currEnemieToHit.transform.position-transform.position;
                //deltaPosition jest zmianą pozycji na płaszczyźnie względem wirzy i przeciwnika. Y wyrzucamy bo nie chcemy żeby wierza sie "Nachylała" do przeciwnika
                deltaPosition.y = 0.0f;
                //krótko mówiąc, unity jest jakie jest i żeby trebusz obracał się w odpowiednią stronę machina musi być obrócona o 180 stopni. Dlatego rotacja w kierunku musi byc obrócona o 180 stopni
                //Teoretycznie można to zmienić w prefabie ale mi sie nie chce z tym bawić :). Dziła działa, jak ruszasz to na swoją własną odpowiedzialność
                machina.transform.rotation = Quaternion.RotateTowards(machina.transform.rotation,  Quaternion.LookRotation(deltaPosition.normalized)*Quaternion.Euler(0,180,0), rotationStepPerSecond*Time.deltaTime);
            }
        }

        protected override void Fire()
        {
            fireTimer -= Time.deltaTime;
            //if tower can fire and have a target
            if (fireTimer <= 0.0 && enemiesToHit.Length > 0)
            {
                currEnemieToHit = enemiesToHit.ElementAt(0);
                animator.SetTrigger("Shoot");
                fireTimer = maxCooldown;
                awaitingBulletFire = true;
            }
            else if (awaitingBulletFire && maxCooldown - fireTimer > maxCooldown * bulletAnimationDelay)
            {
                GameObject tmp = Instantiate(Arrow);
                //call constructor of BasicArrow
                tmp.GetComponent<BasicArrow>().Init(
                    stats.dmgLvl + damageBase,
                    hitRange / arrowTimeToHit,
                    transform.position + BulletOffset,
                    Quaternion.FromToRotation(Vector3.left, transform.position + BulletOffset - currEnemieToHit.transform.position),
                    currEnemieToHit.gameObject);
                awaitingBulletFire = false;
            }
        }
    }
}
