#include <iostream>

class Chain;

class ChainNode
{
	friend class Chain;
	public:
	ChainNode(char element=0, ChainNode *next=0)
	{
		data=element;
		link=next;
	}
	private:
	char data;
	ChainNode *link;
};

class Chain
{
	public:
	void create2()
	{
		ChainNode* _5th = new ChainNode('o', 0);
		ChainNode* _4th = new ChainNode('l', _5th);
		ChainNode* _3rd = new ChainNode('l', _4th);
		ChainNode* second = new ChainNode('e', _3rd);
		first = new ChainNode('h', second);
	}
	
	void print()
	{
		ChainNode *ptr = first;
		while(ptr)
		{
			std::cout<<ptr->data<<" ";
			ptr = ptr->link;
		}
		std::cout<<std::endl;
	}
	
	private:
	ChainNode *first;
};

int main(int argc, char** argv) {
	Chain c;
	c.create2();
	c.print();
	return 0;
}

