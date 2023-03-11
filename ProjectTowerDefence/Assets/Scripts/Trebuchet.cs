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
        private bool canShootAnim;
        private bool performFire;
       

        protected override void Start()
        {
            base.Start();
            animator = transform.Find("Trebuchet.fbx").GetComponent<Animator>();

            transform.Find("Trebuchet.fbx").GetComponent<TrebuchetAnimContr>().setController(this);

            //gimnastyka tutaj jest spowodowana faktem że animacje nie były zrobione szkieletami tylko zmiana rotacji poszczególnych części terbusza
            //a unity nie pozwala ingerować w pliki fbx dlatego skryptem zmieniam hierarchie. 
            GameObject treb = transform.Find("Trebuchet.fbx").gameObject;
            machina = treb.transform.Find("machina").gameObject;
            GameObject wyrzutnia = treb.transform.Find("wyrzutnia").gameObject;
            GameObject tmp = new GameObject("TMPGO");
            tmp.transform.rotation = Quaternion.Euler(0,-90,0);
            tmp.transform.parent = machina.transform;
            wyrzutnia.transform.parent = tmp.transform;


            animator.speed = shootingAnimationLength / maxCooldown;


        }

        protected override void Update()
        {
            base.Update();
            var currentAnimationState = animator.GetCurrentAnimatorStateInfo(0);
            //jeżeli nie ma rpzeciwnika to rotuje sie ... nie rotuje sie 
            if(currEnemieToHit){
                Vector3 deltaPosition = currEnemieToHit.transform.position-transform.position;
                //deltaPosition jest zmianą pozycji na płaszczyźnie względem wirzy i przeciwnika. Y wyrzucamy bo nie chcemy żeby wierza sie "Nachylała" do przeciwnika
                deltaPosition.y = 0.0f;
                //krótko mówiąc, unity jest jakie jest i żeby trebusz obracał się w odpowiednią stronę machina musi być obrócona o -90 stopni. Dlatego rotacja w kierunku musi byc obrócona o -90 stopni
                //Teoretycznie można to zmienić w prefabie ale mi sie nie chce z tym bawić :). Dziła działa, jak ruszasz to na swoją własną odpowiedzialność
                machina.transform.rotation = Quaternion.RotateTowards(machina.transform.rotation,  Quaternion.LookRotation(deltaPosition.normalized)*Quaternion.Euler(0,-90,0), rotationStepPerSecond*Time.deltaTime);
                
            }
        }

        protected override void Fire()
        {
            fireTimer -= Time.deltaTime;
            //if tower can fire and have a target
            if (fireTimer <= 0.0 && enemiesToHit.Length > 0)
            {

                //if enemie exists
                if (currEnemieToHit)
                {
                    animator.SetTrigger("Shoot");
                    fireTimer = maxCooldown;
                }
                else currEnemieToHit = enemiesToHit.ElementAt(0);
            }
        }
        public void callFire(){
            if (currEnemieToHit)
            {
                    
                    GameObject tmp = Instantiate(Arrow);
                    //call  constructor of BasicArrow
                    tmp.GetComponent<BasicArrow>().Init(stats.dmgLvl + damageBase, hitRange / arrowTimeToHit, transform.position + BulletOffset,
                    Quaternion.FromToRotation(Vector3.left, transform.position + BulletOffset - currEnemieToHit.transform.position),
                    currEnemieToHit.gameObject);

            }
            else currEnemieToHit = enemiesToHit.ElementAt(0);
        }
        public void setCanShootAnim(bool setBool)
        {
            canShootAnim = setBool;
        }
        public bool getCanShootAnim(bool setBool)
        {
            return canShootAnim;
        }
        public void setPerformFire(bool setBool)
        {
            performFire = setBool;
        }
        public bool getPerformFire(bool setBool)
        {
            return performFire;
        }
    }
}
